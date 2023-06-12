using InsuranceAPI.Models;
using InsuranceAPI.Repositories;

namespace InsuranceAPI.Services {
    public interface ICompanyService {
        public List<Company> getAll();
        public Company? getById(long id);
    }

    public class CompanyService : ICompanyService{
        private readonly ICompanyRepository _repo;

        public CompanyService(ICompanyRepository repo) {
            _repo = repo;
        }

        public List<Company> getAll() {
            return _repo.getAll();
        }

        public Company? getById(long id) {
            return _repo.getById(id);
        }
    }
}
