using InsuranceAPI.Models;
using InsuranceAPI.Repositories;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Security.Cryptography;
using Org.BouncyCastle.Crypto.Generators;
using BCrypt.Net;
using InsuranceAPI.Exceptions;

namespace InsuranceAPI.Services {
    public interface IAdminService {
        public Task<Admin> authenticate(LoginRequest req);
    }

    public class AdminService : IAdminService{
        private readonly IAdminRepository _repo;
        private readonly IConfiguration _config;

        public AdminService(IAdminRepository adminRepository,
                            IConfiguration config) {
            _repo = adminRepository;
            _config = config;
        }

        public async Task<Admin> authenticate(LoginRequest req) {
            Admin? loggin = _repo.getByUsername(req.Username);

            if (loggin == null)
                throw new BadUserException();

            await Task.Run(() => {
                if(!BCrypt.Net.BCrypt.Verify(req.Password,loggin.Password))
                    throw new BadUserException();

                loggin.Token = generateToken(loggin);
            });
            
            return loggin!;
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
