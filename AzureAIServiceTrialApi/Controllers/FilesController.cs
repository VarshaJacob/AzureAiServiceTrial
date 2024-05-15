using AzureAIServiceTrialApi.Application;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;

namespace AzureAIServiceTrialApi.Controllers
{
    public class FilesController : Controller
    {
        private readonly ILogger<FilesController> _logger;
        private readonly BlobAccess blobAccess;
        public FilesController(ILogger<FilesController> _logger, BlobAccess blobAccess)
        {
            this._logger = _logger;
            this.blobAccess = blobAccess;
        }

        [HttpPost]
        public async Task<IActionResult> PostFiles([FromBody]IBrowserFile file)
        {
            try
            {
                var handler = new PostFilesHandler(blobAccess);
                await handler.HandleAsync(file);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
