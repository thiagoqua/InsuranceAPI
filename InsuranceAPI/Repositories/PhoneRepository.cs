using InsuranceAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace InsuranceAPI.Repositories {
    public interface IPhoneRepository {
        public List<Phone> getAll();
        public Phone getById(long id);
        public List<Phone> getByIds(List<long> id);
        public List<Phone> getByInsured(long insuredId);
        public List<Phone> getByInsureds(List<long> insuredIds);
        public void create(Phone phone);
        public void createMultiple(List<Phone> phones);
        public void delete(long id);
        public void deleteByInsured(long insuredId);
        public bool commit();
    }
    
    public class PhoneRepository : IPhoneRepository{
        private DbInsuranceContext _context;

        public PhoneRepository(DbInsuranceContext context) {
            _context = context;
        }

        public void create(Phone phone) {
            _context.Phones.Add(phone);
        }

        public void createMultiple(List<Phone> phones) {
            foreach(Phone phone in phones)
                create(phone);
        }

        public bool commit() {
            try{
                _context.SaveChanges();
                return true;
            } catch(DbException ex){
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Phone> getAll() {
            return _context.Phones.Include(ph => ph.InsuredNavigation).ToList();
        }

        public Phone getById(long id) {
            return _context.Phones.Include(ph => ph.InsuredNavigation)
                .FirstOrDefault(ph => ph.Id == id);
        }

        public List<Phone> getByIds(List<long> ids) {
            List<Phone> ret = new List<Phone>();
            foreach(long id in ids)
                ret.Add(getById(id));
            return ret;
        }

        public List<Phone> getByInsured(long insuredId) {
            return (from ph in _context.Phones.Include(ph => ph.InsuredNavigation)
                    where ph.InsuredNavigation.Id == insuredId
                    select ph).ToList();
        }

        public List<Phone> getByInsureds(List<long> insuredIds) {
            List<Phone> ret = new List<Phone>();
            foreach(long id in insuredIds)
                ret.Concat(getByInsured(id));
            return ret;
        }

        public void delete(long id) {
            _context.Phones.Remove(getById(id));
        }

        public void deleteByInsured(long insuredId) {
            List<Phone> toDelete = (from phone in _context.Phones
                                    where phone.Insured == insuredId
                                    select phone).ToList();
            _context.Phones.RemoveRange(toDelete);
        }
    }
}
