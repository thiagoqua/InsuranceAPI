using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace InsuranceAPI.Controllers {
    [Authorize]
    [EnableCors("everything")]
    [Route("api/producer")]
    [ApiController]
    public class ProducerController : ControllerBase {
        private readonly IProducerService _service;

        public ProducerController(IProducerService service){
            _service = service;
        }

        /// <summary>Gets all the producers</summary>
        /// <returns>The list of all producers</returns>
        /// <response code="200">Returns the list of all producers</response>
        /// <response code="401">JWT token is missing or invalid</response>
        [ProducesResponseType(typeof(List<Producer>), 200)]
        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> all() {
            List<Producer> ret = await _service.getAll();
            return Ok(ret);
        }
    }
}
