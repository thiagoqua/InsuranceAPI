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
using System.ComponentModel.DataAnnotations;

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

        /// <summary>Map an input file into database objects</summary>
        /// <param name="file">Excel file to parse</param>
        /// <returns>
        ///     An object that contains the list of interpreted insureds and the number of rows
        ///     that hasn't been interpreted
        /// </returns>
        /// <response code="200">
        ///     Returns an object that contains the list of interpreted insureds and the number of rows
        ///     that hasn't been interpreted
        /// </response>
        /// <response code="401">JWT token is missing or invalid</response>
        [ProducesResponseType(typeof(ExcelDataResultDTO), 200)]
        [HttpPost,DisableRequestSizeLimit]
        [Route("upload")]
        public async Task<IActionResult> upload([Required] IFormFile file) {
            ExcelDataResultDTO ret = await _service.parseFile(file);
            return Ok(ret);
        }

        /// <summary>Stores the parsed items</summary>
        /// <remarks>
        ///     The interpreted items are stored in a file called "ultimatum.json". For integration
        ///     and security reasons, that file is deleted 1 minute after it is created.
        ///     This endpoint read the file if exists and insert it into the database.
        /// </remarks>
        /// <response code="200">The changes have been applied successfully</response>
        /// <response code="401">JWT token is missing or invalid</response>
        /// <response code="503">The file where the interpreted insureds are stored doesn't exist anymore</response>
        /// <response code="500">There was an error inserting the changes</response>
        [HttpGet]
        [Route("store")]
        public async Task<IActionResult> store() {
            try {
                await _service.storeParsed();
                return Ok();
            } catch(FileNotFoundException) {
                return StatusCode(503);
            } catch (Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }

        /// <summary>Cancels the data importing process</summary>
        /// <remarks>
        ///     This endpoint deletes the "ultimatum.json" file if existis.
        /// </remarks>
        /// <response code="200">The cancelation has been successfull</response>
        /// <response code="401">JWT token is missing or invalid</response>
        /// <response code="500">There was an error during the cancelation</response>
        [HttpDelete]
        [Route("cancel")]
        public IActionResult cancel() {
            try{
                _service.cancelParsed();
                return Ok();
            } catch(Exception ex) {
                return StatusCode(500,ex.Message);
            }
        }

        /// <summary>
        ///     Exports all the insureds records to a file
        /// </summary>
        /// <param name="PDF">True if the exported report must be in PDF format</param>
        /// <param name="XLSX">True if the exported report must be in Excel format</param>
        /// <returns>The corresponding file containing the data</returns>
        /// <response code="200">Returns the corresponding file containing the data</response>
        /// <response code="400">There is an error in the query params</response>
        /// <response code="401">JWT token is missing or invalid</response>
        /// <response code="500">There was an error creating the file</response>
        [ProducesResponseType(typeof(File), 200)]
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

                return File(result,contentType,fileName);
            } catch(NullReferenceException nre) {
                return StatusCode(500, nre.Message);
            }
        }

        /// <summary>Gets all the backups availables</summary>
        /// <remarks>
        ///     A backup is created after the insureds table is fully dropped. That case happens
        ///     only when importing insureds from a file. 
        /// </remarks>
        /// <returns>The list with all the backup's created dates</returns>
        /// <response code="200">Returns the list with all the backup's created dates</response>
        /// <response code="401">JWT token is missing or invalid</response>
        /// <response code="500">There was an error parsing the backups files</response>
        [ProducesResponseType(typeof(List<string>), 200)]
        [HttpGet]
        [Route("backup/all")]
        public IActionResult getAllBackups() {
            List<string> ret;
            try {
                ret = _service.getBackupsDates();
                return Ok(ret);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }

        /// <summary>Applies the data from a backup to the database</summary>
        /// <param name="name">The backup's date and time to apply</param>
        /// <response code="200">The changes have been applied successfully</response>
        /// <response code="401">JWT token is missing or invalid</response>
        /// <response code="500">There was an error applying changes</response>
        [ProducesResponseType(typeof(List<Insured>), 200)]
        [HttpGet]
        [Route("backup/data")]
        public async Task<IActionResult> applyBackup([FromQuery] [Required] string name) {
            try {
                List<Insured> ret = await _service.getBackupData(name.Replace("+"," "));
                return Ok(ret);
            } catch(Exception ex) {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
