using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace InsuranceAPI.Controllers {
    [EnableCors("everything")]
    [Authorize]
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase {
        private readonly IFileService _service;

        public FileController(IFileService service) {
            _service = service;
        }

        [HttpPost,DisableRequestSizeLimit]
        [Route("upload")]
        public IActionResult upload(IFormFile file) {
            ExcelDataResultDTO ret = _service.parseFile(file);
            return Ok(ret);
        }

        [HttpGet]
        [Route("store")]
        public IActionResult store() {
            try {
                _service.storeParsed();
                return Ok();
            } catch (Exception ex) {
                return StatusCode(
                    StatusCodes.Status500InternalServerError, 
                    ex.Message);
            }
        }

        [HttpDelete]
        [Route("cancel")]
        public IActionResult cancel() {
            try{
                _service.cancelParsed();
                return Ok();
            } catch(Exception ex){
                return StatusCode(StatusCodes.Status500InternalServerError,
                    ex.Message);
            }
        }
    }
}
