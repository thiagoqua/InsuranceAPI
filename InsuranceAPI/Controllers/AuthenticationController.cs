using BCrypt.Net;
using InsuranceAPI.Exceptions;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;

namespace InsuranceAPI.Controllers {
    [EnableCors("everything")]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly IAdminService _service;

        public AuthenticationController(IAdminService service){
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> login(LoginRequest request) {
            try {
                Admin? ret = await _service.authenticate(request);
                return Ok(ret);
            } catch(BadUserException) {
                return BadRequest();
            } catch(Exception) {
                return StatusCode(500);
            }
        }

        //[HttpGet]
        //[Route("password")]
        //public IActionResult createPass([FromQuery] string psw) {
        //    string hash = BCrypt.Net.BCrypt.HashPassword(psw);
        //    return Ok(new { 
        //        hashed = hash,
        //        isEquals = BCrypt.Net.BCrypt.Verify(psw, hash)
        //    });
        //}
    }
}
