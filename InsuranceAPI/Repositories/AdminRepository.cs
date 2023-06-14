using InsuranceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Repositories {
    public interface IAdminRepository {
        public Admin? getByUsername(string username);
    }


    public class AdminRepository : IAdminRepository {
        private DbInsuranceContext _context;
        public AdminRepository(DbInsuranceContext insuranceContext) {
            _context = insuranceContext;
        }
        public Admin? getByUsername(string username) {
            return _context.Admins.Include(admin => admin.ProducerNavigation)
                    .FirstOrDefault(x => x.Username == username);
        }
    }
}
