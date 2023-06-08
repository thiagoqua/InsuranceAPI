using InsuranceAPI.Models;
using System.Data.Common;
using System.Net;

namespace InsuranceAPI.Repositories {
    public interface IAddressRepository {
        public List<Address> getAll();
        public Address getById(long id);
        public List<Address> getByIds(List<long> ids);
        public void create(Address address);
        public void delete(long id);
        public bool commit();
    }

    public class AddressRepository : IAddressRepository {
        private DbInsuranceContext _context;

        public AddressRepository(DbInsuranceContext context){
            _context = context;
        }

        public void create(Address address) {
            _context.Addresses.Add(address);
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

        public List<Address> getAll() {
            return _context.Addresses.ToList();
        }

        public Address getById(long id) { 
            return (from addr in _context.Addresses
                    where addr.Id == id
                    select addr).First();
        }

        public List<Address> getByIds(List<long> ids) {
            List<Address> ret = new List<Address>();
            foreach(long id in ids)
                ret.Add(getById(id));
            return ret;
        }

        public void delete(long id) {
            _context.Addresses.Remove(getById(id));
        }
    }
}
