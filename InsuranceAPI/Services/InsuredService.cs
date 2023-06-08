using InsuranceAPI.Models;
using InsuranceAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Services {
    public interface IInsuredService {
        public List<Insured> getAll();
        public List<Insured> getFromSearch(string query);
        public Insured? getById(long id);
        public bool create(Insured insured);
        public bool update(Insured insured);
        public bool delete(long id);
    }

    public class InsuredService : IInsuredService{
        private readonly IInsuredRepository _insuredRepo;
        private readonly IAddressRepository _addressRepo;
        private readonly IPhoneRepository _phoneRepo;

        public InsuredService(IInsuredRepository repo,IAddressRepository addrRepo,
                                IPhoneRepository phRepo) { 
            _insuredRepo = repo;
            _addressRepo = addrRepo;
            _phoneRepo = phRepo;
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

        public bool create(Insured insured) {
            Address address = insured.AddressNavigation;
            List<Phone> phones = insured.Phones.ToList();

            _addressRepo.create(address);
            _insuredRepo.create(insured);
            _phoneRepo.createMultiple(phones);

            return _insuredRepo.commit();
        }

        public bool update(Insured insured) {
            List<Phone> newPhones = insured.Phones.ToList();
            _insuredRepo.update(insured);
            List<Phone> prevPhones = _phoneRepo.getByInsured(insured.Id);
            foreach(Phone phone in prevPhones){
                if(!newPhones.Exists(ph => ph.Id == phone.Id))
                    _phoneRepo.delete(phone.Id);
            }
            _phoneRepo.commit();
            return _insuredRepo.commit();
        }

        public bool delete(long id) {
            Insured? inCuestion = getById(id);
            bool ret = false;
            if(inCuestion != null){
                _phoneRepo.deleteByInsured(id);
                _insuredRepo.delete(id);
                _addressRepo.delete(inCuestion.Address);
                ret = _insuredRepo.commit();
            }
            return ret;
        }
    }
}
