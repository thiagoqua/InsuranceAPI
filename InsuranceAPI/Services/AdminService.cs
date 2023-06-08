using InsuranceAPI.Models;
using InsuranceAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace InsuranceAPI.Services {
    public interface IAdminService {
        public Admin? authenticate(LoginRequest req);
    }

    public class AdminService : IAdminService{
        private readonly IAdminRepository _repo;
        private readonly IConfiguration _config;

        public AdminService(IAdminRepository adminRepository,
                            IConfiguration config) {
            _repo = adminRepository;
            _config = config;
        }

        public Admin? authenticate(LoginRequest req) {
            Admin? loggin = _repo.authenticate(req);
            if(loggin != null){
                loggin.Token = generateToken(loggin);
            }
            return loggin;
        }

        private string generateToken(Admin admin) {
            var claims = new[]{
                new Claim(ClaimTypes.Name,admin.Username),
                new Claim(ClaimTypes.Surname,
                            admin.ProducerNavigation.Lastname),
                new Claim(ClaimTypes.DateOfBirth,
                            admin.ProducerNavigation.Joined.ToShortDateString())
            };

            var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_config.GetSection("JWT:key").Value)
            );

            var creds = new SigningCredentials(key,
                SecurityAlgorithms.HmacSha512Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler()
                    .WriteToken(token);
        }
    }

}
