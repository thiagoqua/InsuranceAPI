using Azure;
using InsuranceAPI.Models;
using InsuranceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.Net.Http.Headers;
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
        public async Task<IActionResult> upload(IFormFile file) {
            ExcelDataResultDTO ret = await _service.parseFile(file);
            return Ok(ret);
        }

        [HttpGet]
        [Route("store")]
        public async Task<IActionResult> store() {
            try {
                await _service.storeParsed();
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

        [HttpGet]
        [Route("export")]
        public async Task<IActionResult> export([FromQuery] bool? PDF, 
                                                [FromQuery] bool? XLSX) {
            exportingFormats format;

            if(XLSX != null && PDF == null)
                format = exportingFormats.XLSX;
            else if(PDF != null && XLSX == null)
                format = exportingFormats.PDF;
            else
                return BadRequest();

            try {
                byte[] result = await _service.exportAsync(format);
                string contentType, fileName;

                if(format == exportingFormats.XLSX) {
                    contentType = "application/vnd.ms-excel";
                    fileName = "cartera_asegurado.xlsx";
                } else {
                    contentType = "application/pdf";
                    fileName = "cartera_asegurado.pdf";
                }

                return File(
                    result,
                    contentType,
                    fileName
                );
            } catch(NullReferenceException nre) {
                return StatusCode(500, nre.Message);
            }
        }

        [HttpGet]
        [Route("backup/all")]
        public IActionResult getAllBackups() {
            List<string> ret;
            try {
                ret = _service.getBackups();
                return Ok(ret);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("backup/proceed")]
        public async Task<IActionResult> applyBackup([FromQuery] string backup) {
            try {
                await _service.applyBackup(backup.Replace("+"," "));
                return Ok();
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
