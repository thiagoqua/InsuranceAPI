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

        /// <summary>Gets all the companies</summary>
        /// <returns>The list of all companies</returns>
        /// <response code="200">Returns the list of all companies</response>
        /// <response code="401">JWT token is missing or invalid</response>
        [HttpGet]
        [Route("all")]
        public IActionResult all() {
            return Ok(_service.getAll());
        }
    }
}
