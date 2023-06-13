using InsuranceAPI.Models;
using InsuranceAPI.Exceptions;
using InsuranceAPI.Repositories;
using System.Text.Json;
using InsuranceAPI.Helpers;
using NPOI.SS.UserModel;

namespace InsuranceAPI.Services {
    public interface IFileService {
        public ExcelDataResultDTO parseFile(IFormFile file);
        public void storeParsed();
        public void cancelParsed();
        public IWorkbook exportToExcel();
        public void exportToPDF();
    }

    public class FileService : IFileService {
        private readonly ICompanyService _companyService;
        private readonly IProducerRepository _producerRepository;
        private readonly IInsuredService _insuredService;
        private readonly string filesDir;
        private readonly JsonSerializerOptions JSONopt;
        private static CancellationTokenSource _cancelationToken;

        public FileService(IProducerRepository producerRepository,
                            ICompanyService companyService,
                            IInsuredService insuredService) {
            _producerRepository = producerRepository;
            _companyService = companyService;
            _insuredService = insuredService;
            filesDir = Path.Combine(Environment.CurrentDirectory,"Files");
            JSONopt = new JsonSerializerOptions() {PropertyNameCaseInsensitive = true};
        }

        public ExcelDataResultDTO parseFile(IFormFile excelFile) {
            ExcelHandler handler = new ExcelHandler(
                excelFile,
                _producerRepository.getAll(),
                _companyService.getAll()
            );
            ExcelDataResultDTO parsingResult = handler.parse();
            string fullDir = Path.Combine(filesDir,"ultimatum.json");

            //creating the ultimatum file
            Task.Run(() => {
                if(File.Exists(fullDir))
                    cancelParsed();
                using(FileStream fstream = new FileStream(fullDir,
                                                    FileMode.CreateNew)){
                    List<Insured> toStore = new List<Insured>();
                    //removing the company object,
                    //using a deep clone of that
                    foreach(Insured insured in parsingResult.Interpreted){
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
            return parsingResult;
        }

        public void storeParsed() {
            _cancelationToken!.Cancel();
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
            _cancelationToken!.Cancel();
            removeUltimatum();
        }

        public IWorkbook exportToExcel() {
            ExcelHandler handler = new ExcelHandler();


            return handler.export(_insuredService.getAll());

        }

        public void exportToPDF() {
            
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
                .ContinueWith(t => removeUltimatum(), 
                    TaskContinuationOptions.OnlyOnRanToCompletion);
        }
    }
}
