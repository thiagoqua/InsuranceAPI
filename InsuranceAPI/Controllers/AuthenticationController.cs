using BCrypt.Net;
using InsuranceAPI.Exceptions;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Crypto.Generators;
using System.ComponentModel.DataAnnotations;

namespace InsuranceAPI.Controllers {
    [EnableCors("everything")]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly IAdminService _service;

        public AuthenticationController(IAdminService service){
            _service = service;
        }

        /// <summary>Authenticates an admin user</summary>
        /// <param name="request">The request payload</param>
        /// <response code="200">User's authentication succesfull</response>
        /// <response code="400">User not found</response>
        /// <response code="500">There was an error in the authentication process</response>
        /// <returns>The requester' admin data</returns>
        [HttpPost]
        public async Task<IActionResult> login([FromBody] [Required] LoginRequest request) {
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
