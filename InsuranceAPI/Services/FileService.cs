using InsuranceAPI.Models;
using InsuranceAPI.Exceptions;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System.Reflection.Metadata.Ecma335;
using InsuranceAPI.Repositories;

namespace InsuranceAPI.Services {
    public interface IFileService {
        public List<Insured>? parseFile(IFormFile file);
    }

    public class FileService : IFileService {
        private readonly IProducerRepository _producerRepository;

        public FileService(IProducerRepository producerRepository) {
            _producerRepository = producerRepository;
        }

        public List<Insured>? parseFile(IFormFile excelFile) {
            List<Insured> ret = new List<Insured>();
            Stream stream = excelFile.OpenReadStream();
            IWorkbook workBook;
            ISheet sheet;
            IRow row;
            int rows;
            string extension = Path.GetExtension(excelFile.FileName);

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
                    ret.Add(mapFromExcelRow(row,i));
                }
                catch(MappingException me) {
                    Console.WriteLine(
                        "error en fila " + me.ErrorRow + 
                        " en campo " + me.ErrorCell
                    );
                }
            }
            stream.Close();
            return ret;
        }

        private Insured mapFromExcelRow(IRow row,int rowNumber) {
            try {
                string[]? namesPolicy = mapNameOrPolicyFromExcelRow(row.GetCell(3).ToString());
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
                    Producer = mapProducerFromExcelRow(row.GetCell(13).ToString()),
                    Company = mapCompanyFromExcelRow(row.GetCell(1).CellStyle.FillBackgroundColorColor),
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

        private long mapProducerFromExcelRow(string? cell) {
            if(cell == null)
                throw new MappingException("producer");
            long? ret = _producerRepository.getIdByFirstName(cell.ToLower());
            
            if(ret == null)
                throw new MappingException("producer");

            return (long) ret;
        }

        private long mapCompanyFromExcelRow(IColor color) {
            Console.WriteLine("color is " + color.RGB);
            return 1;
        }
    }
}
