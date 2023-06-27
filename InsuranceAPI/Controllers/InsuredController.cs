using Microsoft.AspNetCore.Mvc;
using InsuranceAPI.Services;
using InsuranceAPI.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Authorization;
using System.ComponentModel.DataAnnotations;

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

        /// <summary>Gets all the insureds</summary>
        /// <returns>The list of all insureds</returns>
        /// <response code="200">Returns the list of all insureds</response>
        /// <response code="401">JWT token is missing or invalid</response>
        [HttpGet]
        [Route("all")]
        public IActionResult all() { 
            return Ok(_service.getAll());
        }

        /// <summary>Looks for an insured</summary>
        /// <param name="query">The input string to match</param>
        /// <returns>
        ///     The list with the insureds whose firstname or surname matches the query
        /// </returns>
        /// <response code="200">
        ///     The list with the insureds whose firstname or surname matches the query.
        ///     If there is not insureds to match, returns an empty list.
        /// </response>
        /// <response code="401">JWT token is missing or invalid</response>
        [HttpGet]
        [Route("search")]
        public IActionResult match([FromQuery] [Required] string query) {
            return Ok(_service.getFromSearch(query));
        }

        /// <summary>Gets an insured by its id</summary>
        /// <param name="id">The requested insured's id</param>
        /// <returns>The insured requested</returns>
        /// <response code="200">Returns the insured requested</response>
        /// <response code="401">JWT token is missing or invalid</response>
        /// <response code="404">An insured with the given id is not found</response>
        [HttpGet]
        [Route("{id:long}")]
        public IActionResult getById(long id) {
            var ret = _service.getById(id);
            return ret != null ?
                    Ok(ret) : 
                    NotFound();
        }

        /// <summary>Creates a new insured</summary>
        /// <param name="insured">The new insured's payload</param>
        /// <response code="200">The insured was created successfully</response>
        /// <response code="400">The given payload is null</response>
        /// <response code="401">JWT token is missing or invalid</response>
        /// <response code="500">There was an error creating the insured</response>
        [HttpPost]
        [Route("new")]
        public IActionResult create([FromBody] [Required] Insured insured) {
            if(insured == null)
                return BadRequest();
            
            try {
                _service.create(insured,true);
                return Ok();
            } catch(Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }

        /// <summary>Update the insured's data</summary>
        /// <param name="insured">The updated insured payload</param>
        /// <response code="200">The changes has been applied successfully</response>
        /// <response code="400">The given payload is null</response>
        /// <response code="401">JWT token is missing or invalid</response>
        /// <response code="500">There was an error updating the insured</response>
        [HttpPut]
        [Route("update")]
        public IActionResult update([FromBody] [Required] Insured insured) {
            if(insured == null)
                return BadRequest();

            try {
                _service.update(insured);
                return Ok();
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>Deletes an insured by its id</summary>
        /// <param name="id">The insured's id to be deleted</param>
        /// <response code="200">The insured has been deleted successfully</response>
        /// <response code="401">JWT token is missing or invalid</response>
        /// <response code="500">There was an error deleting the insured</response>
        [HttpDelete]
        [Route("delete/{id:long}")]
        public IActionResult delete(long id) {
            try {
                _service.delete(id,true);
                return Ok();
            } catch(Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet]
        [Route("test")]
        public IActionResult test() {
            return Ok("funca?");
        }
    }
}
