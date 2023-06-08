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
        public IActionResult reader(IFormFile file) {
            List<Insured>? ret = _service.parseFile(file);
            return ret != null ? Ok(ret) : StatusCode(StatusCodes.Status406NotAcceptable);
        }
    }
}
