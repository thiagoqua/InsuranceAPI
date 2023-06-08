using InsuranceAPI.Models;

namespace InsuranceAPI.Repositories {
    public interface ICompanyRepository {
        public List<Company> getAll();
    }

    public class CompanyRepository : ICompanyRepository {
        private DbInsuranceContext _context;

        public CompanyRepository(DbInsuranceContext context) { 
            _context = context;
        }

        public List<Company> getAll() {
            return _context.Companies.ToList();
        }
    }
}
