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
        /// <response code="200">Returns all the insureds</response>
        /// <response code="401">JWT token is missing or invalid</response>
        [HttpGet]
        [Route("all")]
        [ProducesResponseType(typeof(List<Insured>), 200)]
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
        [ProducesResponseType(typeof(List<Insured>), 200)]
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
        [ProducesResponseType(typeof(Insured), 200)]
        public IActionResult getById(long id) {
            var ret = _service.getById(id);
            return ret != null ?
                    Ok(ret) : 
                    NotFound();
        }

        /// <summary>Gets the insureds with the given filters</summary>
        /// <param name="company" example="1">The comany's id to filter</param>
        /// <param name="producer" example="2">The producer's id to filter</param>
        /// <param name="lifestart" example="18+05">The starting insured's policy life to filter</param>
        /// <param name="status" example="ANULADA">The insured's policy status to filter</param>
        /// <response code="200">The insured's list according the filters</response>
        /// <response code="400">All the filters or one of them are not correct</response>
        /// <response code="401">JWT token is missing or invalid</response>
        [HttpGet]
        [Route("filter")]
        [ProducesResponseType(typeof(List<Insured>), 200)]
        public async Task<IActionResult> filter(
            [FromQuery] string? company, [FromQuery] string? producer,
            [FromQuery] string? lifestart, [FromQuery] string? status) {
            try {
                List<Insured> ret = await _service.getByFilters(new string?[] {
                        company, producer, lifestart, status
                });

                return Ok(ret);
            } catch(ArgumentException) {
                return BadRequest();
            }
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
    }
}
