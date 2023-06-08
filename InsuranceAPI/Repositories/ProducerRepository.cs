using InsuranceAPI.Models;

namespace InsuranceAPI.Repositories {
    public interface IProducerRepository {
        public List<Producer> getAll();
        public Producer getById(long id);
        public List<Producer> getByIds(List<long> ids);
        public Producer? getByName(string name);
        public long? getIdByFirstName(string name);
    }

    public class ProducerRepository : IProducerRepository{
        private DbInsuranceContext _context;

        public ProducerRepository(DbInsuranceContext context) { 
            _context = context;
        }

        public List<Producer> getAll() {
            return _context.Producers.ToList();
        }

        public Producer getById(long id) {
            return (from prod in _context.Producers
                    where prod.Id == id
                    select prod).First();
        }

        public List<Producer> getByIds(List<long> ids) {
            List<Producer> ret = new List<Producer>();
            foreach(long id in ids) 
                ret.Add(getById(id));
            return ret;
        }

        public Producer? getByName(string name) {
            return _context.Producers
                .FirstOrDefault(prod => prod.Firstname.StartsWith(name) ||
                                        prod.Lastname.StartsWith(name) ||
                                        (prod.Firstname + " " + prod.Lastname)
                                            .StartsWith(name));
        }

        public long? getIdByFirstName(string firstName) {
            Producer? ret = _context.Producers.FirstOrDefault(prod => 
                                        prod.Firstname.StartsWith(firstName));
            if(ret != null)
                return ret.Id;
            
            return null;
        }
    }
}
