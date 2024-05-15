using AzureAiServiceTrial;
using AzureAIServiceTrialApi.Application;
using Microsoft.AspNetCore.Mvc;

namespace AzureAIServiceTrialApi.Controllers
{
    [Route("api/[controller]")]
    public class WebScraperController: ControllerBase
    {
        private readonly ILogger<WebScraperController> _logger;
        private readonly AIService aIService;
        private readonly BlobAccess blobAccess;

        public WebScraperController(ILogger<WebScraperController> logger, AIService aIService, BlobAccess blobAccess)
        {
            this._logger = logger;
            this.aIService = aIService;
            this.blobAccess = blobAccess;

        }

        [HttpGet]
        public async Task<IActionResult> GetWebScraper([FromRoute] string url)
        {
            try
            {
                var handler = new GetWebScraperHandler(aIService, blobAccess);
                await handler.HandleAsync(url);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
