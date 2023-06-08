using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers {
    [EnableCors("everything")]
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase {
        private readonly IAdminService _service;

        public AuthenticationController(IAdminService service){
            _service = service;
        }

        [HttpPost()]
        public IActionResult login(LoginRequest request) {
            Admin? ret = _service.authenticate(request);
            return ret != null ? Ok(ret) : BadRequest();
        }
    }
}
