using InsuranceAPI.Models;
using InsuranceAPI.Exceptions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Reflection.Metadata.Ecma335;
using InsuranceAPI.Repositories;
using NPOI.XWPF.UserModel;
using System.Text.Json;
using Org.BouncyCastle.Crypto.Parameters;

namespace InsuranceAPI.Services {
    public interface IFileService {
        public ExcelDataResultDTO parseFile(IFormFile file);
        public void storeParsed();
        public void cancelParsed();
    }

    public class FileService : IFileService {
        private readonly IProducerRepository _producerRepository;
        private readonly ICompanyRepository _companyRepository;
        private readonly IInsuredService _insuredService;
        private readonly string filesDir = Path.Combine(
            System.Environment.CurrentDirectory,
            "Files");
        private readonly JsonSerializerOptions JSONopt = new JsonSerializerOptions(){
            PropertyNameCaseInsensitive = true
        };
        private static CancellationTokenSource _cancelationToken;

        public FileService(IProducerRepository producerRepository,
                            ICompanyRepository companyRepository,
                            IInsuredService insuredService) {
            _producerRepository = producerRepository;
            _companyRepository = companyRepository;
            _insuredService = insuredService;
        }

        public ExcelDataResultDTO parseFile(IFormFile excelFile) {
            List<Insured> interpreted = new List<Insured>();
            List<int> nonInterpretedRows = new List<int>();
            Stream stream = excelFile.OpenReadStream();
            IWorkbook workBook;
            ISheet sheet;
            IRow row;
            int rows;
            string extension = Path.GetExtension(excelFile.FileName);
            string fullDir = Path.Combine(filesDir,"ultimatum.json");

            if(extension == ".xlsx")
                workBook = new XSSFWorkbook(stream);
            else
                workBook = new HSSFWorkbook(stream);

            sheet = workBook.GetSheetAt(0);
            rows = sheet.LastRowNum;
            //modify the i starting value for the corresponding first row of data
            for(int i = 1; i < rows; i++){
                row = sheet.GetRow(i);
                try {
                    interpreted.Add(mapFromExcelRow(row,i));
                }
                catch(Exception) {
                    nonInterpretedRows.Add(i);
                }
            }
            stream.Close();

            Task.Run(() => {
                using(FileStream fstream = new FileStream(fullDir, 
                                                    FileMode.CreateNew)){
                    List<Insured> toStore = new List<Insured>();
                    //removing the company object,
                    //using a deep clone of that
                    foreach(Insured insured in interpreted){
                        string serialized = JsonSerializer.Serialize(insured);
                        Insured toAdd = JsonSerializer
                            .Deserialize<Insured>(serialized)!;
                        toAdd.CompanyNavigation = null!;
                        toAdd.ProducerNavigation = null!;
                        toStore.Add(toAdd);
                    }
                    JsonSerializer.Serialize(fstream, toStore, JSONopt);
                }
            });
            startTimer();
            return new ExcelDataResultDTO(interpreted,nonInterpretedRows);
        }

        public void storeParsed() {
            List<Insured>? toSave;
            List<Insured> previous = _insuredService.getAll();
            string ultimatumDir, json, backupDir, fileBackupDir;

            ultimatumDir = Path.Combine(filesDir, "ultimatum.json");
            json = File.ReadAllText(ultimatumDir);
            backupDir = Path.Combine(filesDir, "Backups");
            fileBackupDir = Path.Combine(
                backupDir,
                DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".json"
            );

            if(!File.Exists(ultimatumDir))
                throw new Exception("there is no parsed file to store");

            toSave = JsonSerializer.Deserialize<List<Insured>>(json, JSONopt);

            if(toSave == null)
                throw new MappingException("JSON_deserializing");

            //create the backup
            using(FileStream fstream = new FileStream(fileBackupDir, FileMode.CreateNew)) {
                JsonSerializer.Serialize(fstream, previous, JSONopt);
            }

            _insuredService.deleteMultiple(previous.Select(ins => ins.Id)
                                                    .ToList());
            _insuredService.createMultiple(toSave);
            
            File.Delete(ultimatumDir);
        }

        public void cancelParsed() {
            _cancelationToken?.Cancel();
            removeUltimatum();
        }

        private void removeUltimatum() {
            string path = Path.Combine(filesDir, "ultimatum.json");

            if(!File.Exists(path))
                throw new Exception("there is no parsed file to remove");

            File.Delete(path);
        }

        /// <summary>
        ///     This function starts a timer and wait 1 minute after the user has parsed
        ///     the file. If the user doesn't cancel or confirm the parse, it deletes the
        ///     'ultimatum.json' file.
        /// </summary>
        private void startTimer() {
            _cancelationToken = new CancellationTokenSource();
            var timer = Task.Delay(TimeSpan.FromMinutes(1), _cancelationToken.Token)
                .ContinueWith(t => removeUltimatum());
        }

        private Insured mapFromExcelRow(IRow row,int rowNumber) {
            try {
                string[]? namesPolicy = mapNameOrPolicyFromExcelRow(row.GetCell(3).ToString());
                Company company = mapCompanyFromExcelRow(row.GetCell(0).CellStyle.FillForegroundColorColor);
                Producer producer = mapProducerFromExcelRow(row.GetCell(13).ToString());
                Insured ret = new Insured() {
                    License = mapStringFromExcelRow(row.GetCell(0).ToString()),
                    Folder = mapFolderFromExcelRow(row.GetCell(1).ToString()),
                    Life = mapStringFromExcelRow(row.GetCell(2).ToString()),
                    Firstname = namesPolicy[0],
                    Lastname = namesPolicy[1],
                    InsurancePolicy = namesPolicy[2] != null ? namesPolicy[2] : null,
                    Born = mapBornFromExcelRow(row.GetCell(4).ToString()),
                    AddressNavigation = mapAddressFromExcelRow(row),
                    Dni = mapDNIFromExcelRow(row.GetCell(9).ToString()),
                    Phones = mapPhonesFromExcelRow(row.GetCell(10).ToString()),
                    Description = mapDescriptionFromExcelRow(row.GetCell(11).ToString()),
                    Cuit = row.GetCell(12).ToString(),
                    Producer = producer.Id,
                    ProducerNavigation = producer,
                    CompanyNavigation = company,
                    Company = company.Id,
                    Status = mapStringFromExcelRow(row.GetCell(6).ToString())
                };
                return ret;
            } catch (MappingException me) {
                me.ErrorRow = rowNumber;
                throw me;
            }
        }

        private string mapStringFromExcelRow(string? cell) {
            if(cell != null)
                return cell;
            throw new MappingException("license");
        }

        private int mapFolderFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("folder");

            if(cell.Trim().Equals("SIN CARPETA"))
                return 0;
            
            try{
                return int.Parse(cell);
            } catch (Exception e){
                throw new MappingException("folder");
            }
        }

        private DateTime mapBornFromExcelRow(string? cell) {
            if(cell != null)
                return DateTime.Parse(cell);
            throw new MappingException("born");
        }

        private string mapDNIFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("DNI");

            if(cell.StartsWith("DNI "))
                return cell.Split(" ")[1];
            else if(cell.StartsWith("LE"))
                return cell;

            throw new MappingException("DNI");
        }

        private string? mapDescriptionFromExcelRow(string? cell) {
            if(cell != null)
                return cell.Replace("(", "").Replace(")", "");
            return cell;
        }

        private string[] mapNameOrPolicyFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("name");

            string[] completeNameAndPolicy = cell.Split(" ");
            int policyIndex = -1;

            if(completeNameAndPolicy.Length < 2)
                throw new MappingException("name");

            for(int i = 0;i < completeNameAndPolicy.Length; ++i){
                //becouse the description is inside braces
                if(completeNameAndPolicy[i].StartsWith("(")){
                    policyIndex = i;
                    break;
                }
            }
            
            //setting the first name
            string completeFirstName = completeNameAndPolicy[1];
            for(int i = 2; i < policyIndex; ++i)
                completeFirstName += " " + completeNameAndPolicy[i];
            //setting the last name
            string lastname = completeNameAndPolicy[0];
            //setting the policy if exists
            string? policy;
            if(policyIndex != -1){
                string aux = completeNameAndPolicy[policyIndex];
                for(int i = policyIndex + 1; i < completeNameAndPolicy.Length; ++i)
                    aux += " " + completeNameAndPolicy[i];
                policy = aux.Replace("(", "").Replace(")", "");
            }
            else
                policy = null;

            return new string[] { completeFirstName, lastname, policy };
        }
        /// <summary>
        ///     It receives 
        /// </summary>
        /// <param name="row">Excel document's row</param>
        /// <returns>An address with the data in the row</returns>
        /// <exception cref="MappingException">
        ///     If cell 5 doesn't have the format: _street _number P _floor DTO _dpt.
        /// </exception>
        private Address mapAddressFromExcelRow(IRow row) {
            string[]? cell = row.GetCell(5).ToString()?.Split(" ");
            string? street, number, city, province, country;
            int numberStartIndex,floor,departament;

            city = mapStringFromExcelRow(row.GetCell(8).ToString());
            province = "Santa Fe";
            country = "Argentina";
            numberStartIndex = floor = departament = -1;

            if(cell == null || cell.Length < 2)
                throw new MappingException("dirección");

            //getting the number
            for(int i = 0;i < cell.Length;++i){
                if(char.IsDigit(cell[i], 0) || cell[i].Equals("S/N")){
                    numberStartIndex = i;
                    break;
                }
            }

            if(numberStartIndex == -1)
                throw new MappingException("número_dirección");

            //getting the floor and departament if exists
            try{
                for(int i = numberStartIndex + 1; i < cell.Length; ++i)
                {
                    if(cell[i].Equals("P"))
                        floor = int.Parse(cell[i + 1]);
                    else if(cell[i].Equals("DTO"))
                        departament = int.Parse(cell[i + 1]);
                }
            } catch (Exception){
                throw new MappingException("piso_departamento");
            }

            //setting values
            //all the strings before the number are the street
            street = "";
            for(int i = 0; i < numberStartIndex; ++i)
                street += " " + cell[i];

            //all the strings between the number and the floor or departament are the number
            number = "";
            for(int i = numberStartIndex;i < cell.Length; ++i){
                if(!cell[i].Equals("P") && !cell[i].StartsWith("DTO"))
                    number += " " + cell[i];
                else
                    break;
            }

            return new Address() { 
                Street = street,
                Number = number,
                Floor = floor != -1 ? floor : null,
                Departament = departament != -1 ? departament : null,
                City = city,
                Province = province,
                Country = country
            };
        }

        private List<Phone> mapPhonesFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("phones");

            List<Phone> ret = new List<Phone>();
            string[] phones = cell.Split('/');
            Phone toAdd;

            foreach(string phone in phones) {
                //contains a description
                if(phone.Contains("(")) {
                    string[] numberAndDescription = phone.Split("(");
                    toAdd = new Phone() {
                        Number = numberAndDescription[0],
                        Description = numberAndDescription[1]
                                        .Replace("(", "")
                                        .Replace(")", "")
                    };
                }
                else
                    toAdd = new Phone() {
                        Number = phone
                    };
                ret.Add(toAdd);
            }
            return ret;
        }

        private Producer mapProducerFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("producer");

            Producer? ret = _producerRepository.getByName(cell.ToLower());
            
            if(ret == null)
                throw new MappingException("producer");

            return ret;
        }

        private Company mapCompanyFromExcelRow(IColor backgroundColor) {
            Company? ret;
            if(backgroundColor == null)
                throw new MappingException("company");
            byte R, G, B;
            R = backgroundColor.RGB[0];
            G = backgroundColor.RGB[1];
            B = backgroundColor.RGB[2];
            //if the cell's color is white, the company is Federación
            if(R == 255 && G == 255 && B == 255)
                ret = _companyRepository.getById(2);
            else
                ret = _companyRepository.getById(1);

            if(ret == null)
                throw new MappingException("company");

            return ret;
        }
    }
}
