using Microsoft.AspNetCore.Mvc;
using InsuranceAPI.Services;
using InsuranceAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;

namespace InsuranceAPI.Controllers {
    [Authorize]
    [EnableCors("everything")]
    [Route("api/insured")]
    [ApiController]
    public class InsuredController : ControllerBase {
        private readonly IInsuredService _service;

        public InsuredController(IInsuredService service){
            _service = service;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult all() { 
            return Ok(_service.getAll());
        }

        [HttpGet]
        [Route("search")]
        public IActionResult match([FromQuery] string query) {
            return Ok(_service.getFromSearch(query));
        }

        [HttpGet]
        [Route("{id:long}")]
        public IActionResult getById(long id) {
            var ret = _service.getById(id);
            return ret != null ?
                    Ok(ret) : 
                    NotFound();
        }

        [HttpPost]
        [Route("new")]
        public IActionResult create([FromBody] Insured insured) {
            if(insured == null)
                return BadRequest();
            bool ret = _service.create(insured,true);
            return ret ? Ok() : StatusCode(500);
        }

        [HttpPut]
        [Route("update")]
        public IActionResult update([FromBody] Insured insured) {
            if(insured == null)
                return BadRequest();
            bool ret = _service.update(insured);
            return ret ? Ok() : StatusCode(500);
        }

        [HttpDelete]
        [Route("delete/{id:long}")]
        public IActionResult delete(long id) {
            bool ret = _service.delete(id,true);
            return ret ? Ok() : StatusCode(500);
        }

        [HttpGet]
        [Route("test")]
        public IActionResult test() {
            return Ok("funca?");
        }
    }
}
