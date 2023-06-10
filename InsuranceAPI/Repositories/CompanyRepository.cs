using InsuranceAPI.Models;

namespace InsuranceAPI.Repositories {
    public interface ICompanyRepository {
        public List<Company> getAll();
        public Company? getById(long id);
    }

    public class CompanyRepository : ICompanyRepository {
        private DbInsuranceContext _context;

        public CompanyRepository(DbInsuranceContext context) { 
            _context = context;
        }

        public List<Company> getAll() {
            return _context.Companies.ToList();
        }

        public Company? getById(long id) {
            return _context.Companies.FirstOrDefault(c => c.Id == id);
        }
    }
}
