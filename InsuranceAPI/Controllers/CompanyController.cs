using InsuranceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers {
    [Authorize]
    [EnableCors("everything")]
    [Route("api/company")]
    [ApiController]
    public class CompanyController : ControllerBase {
        private ICompanyService _service;

        public CompanyController(ICompanyService service) {
            _service = service;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult all() {
            return Ok(_service.getAll());
        }
    }
}
