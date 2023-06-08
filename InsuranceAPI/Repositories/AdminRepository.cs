using InsuranceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace InsuranceAPI.Repositories {
    public interface IAdminRepository {
        public Admin? authenticate(LoginRequest admin);
    }


    public class AdminRepository : IAdminRepository {
        private DbInsuranceContext _context;
        public AdminRepository(DbInsuranceContext insuranceContext) {
            _context = insuranceContext;
        }
        public Admin? authenticate(LoginRequest request) {
            return (from admin in _context.Admins.Include(ad => ad.ProducerNavigation)
                    where admin.Username == request.username &&
                            admin.Password == request.password
                    select admin).FirstOrDefault();
        }
    }
}
