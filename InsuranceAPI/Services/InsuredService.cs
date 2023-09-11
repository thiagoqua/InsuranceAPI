using InsuranceAPI.Helpers;
using InsuranceAPI.Models;
using InsuranceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Esf;
using System.ComponentModel.Design;

namespace InsuranceAPI.Services {
    public interface IInsuredService {
        public List<Insured> getAll();
        public List<Insured> getFromSearch(string query);
        public Insured? getById(long id);
        public void create(Insured insured,bool commit);
        public void createMultiple(List<Insured> insureds);
        public void update(Insured insured);
        public void delete(long id,bool commit);
        public void deleteMultiple(List<long> insuredIds);
        public Task<List<Insured>> getByFilters(string?[] filters);
    }

    public class InsuredService : IInsuredService{
        private readonly IInsuredRepository _insuredRepo;
        private readonly IAddressRepository _addressRepo;
        private readonly IPhoneRepository _phoneRepo;
        private readonly ICompanyRepository _companyRepo;
        private readonly IProducerRepository _producerRepo;

        public InsuredService(IInsuredRepository repo,IAddressRepository addrRepo,
                                IPhoneRepository phRepo,ICompanyRepository companyRepo,
                                IProducerRepository producerRepo) { 
            _insuredRepo = repo;
            _addressRepo = addrRepo;
            _phoneRepo = phRepo;
            _companyRepo = companyRepo;
            _producerRepo = producerRepo;
        }

        public List<Insured> getAll() {
            return _insuredRepo.getAll();
        }

        public List<Insured> getFromSearch(string queryParam) {
            string query = queryParam.Replace('+', ' ');
             return _insuredRepo.search(query);
        }

        public Insured? getById(long id) {
            return _insuredRepo.findById(id);
        }

        public void create(Insured insured,bool commit) {
            Address address = insured.AddressNavigation;
            List<Phone> phones = insured.Phones.ToList();

            InsuredValidator.EnsureValidInsured(insured);
            
            _addressRepo.create(address);
            _insuredRepo.create(insured);
            _phoneRepo.createMultiple(phones);

            if(commit)
                _insuredRepo.commit();
        }

        public void update(Insured insured) {
            List<Phone> newPhones = insured.Phones.ToList();
            _insuredRepo.update(insured);
            List<Phone> prevPhones = _phoneRepo.getByInsured(insured.Id);
            foreach(Phone phone in prevPhones){
                if(!newPhones.Exists(ph => ph.Id == phone.Id))
                    _phoneRepo.delete(phone.Id);
            }
            _phoneRepo.commit();
            _insuredRepo.commit();
        }

        public void delete(long id, bool commit) {
            Insured? inCuestion = getById(id);
            if(inCuestion != null){
                _phoneRepo.deleteByInsured(id);
                _insuredRepo.delete(id);
                _addressRepo.delete(inCuestion.Address);
                if(commit) {
                    _insuredRepo.commit();
                }
            }
        }

        public void createMultiple(List<Insured> insureds) {
            foreach(Insured newOne in insureds) 
                create(newOne, false);

            _insuredRepo.commit();
        }

        public void deleteMultiple(List<long> insuredIds) {
            foreach(long id in insuredIds)
                delete(id, false);

            _insuredRepo.commit();
        }

        private bool checkLifeFormat(string life) {
            //format is dd/mm
            if(life.Length != 5)
                return false;
            if(life.ElementAt(2) != '/')
                return false;

            foreach(char c in life) {
                if(c == '/') 
                    continue;
                if(!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        public async Task<List<Insured>> getByFilters(string?[] filters) {
            bool allNulls = true;
            
            foreach(string? filter in filters) {
                allNulls = allNulls && filter == null;
            }

            if(allNulls)
                throw new ArgumentException();

            return await Task.Run(() => {
                IQueryable<Insured> allInsureds = _insuredRepo.getAllQueryables();

                // COMPANY FILTER
                if(filters[0] != null) {     
                    long companyId = Convert.ToInt64(filters[0]);
                    if(_companyRepo.getById(companyId) == null)
                        throw new ArgumentException();

                    allInsureds = allInsureds
                        .Where(ins => ins.Company == companyId);
                }

                // PRODUCER FILTER
                if(filters[1] != null) {
                    long producerId = Convert.ToInt64(filters[1]);
                    if(_producerRepo.getById(producerId) == null)
                        throw new ArgumentException();

                    allInsureds = allInsureds
                        .Where(ins => ins.Producer == producerId);
                }

                // LIFE START FILTER
                if(filters[2] != null) {
                    string life = filters[2].Replace(' ','/');
                    if(!checkLifeFormat(life))
                        throw new ArgumentException();

                    allInsureds = allInsureds
                        .Where(ins => ins.Life.StartsWith(life));
                }

                // POLICY STATUS FILTER
                if(filters[3] != null) {
                    if(!(filters[3] == "ACTIVA") &&
                       !(filters[3] == "EN JUICIO")&&
                       !(filters[3] == "ANULADA"))
                        throw new ArgumentException();
                    allInsureds = allInsureds
                        .Where(ins => ins.Status == filters[3]);
                }

                return allInsureds.ToList();
            });
        }
    }
}
