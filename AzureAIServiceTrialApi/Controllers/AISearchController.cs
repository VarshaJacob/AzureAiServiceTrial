using AzureAIServiceTrialApi.Application;
using Microsoft.AspNetCore.Mvc;

namespace AzureAIServiceTrialApi.Controllers
{
    [Route("api/[controller]")]
    public class AISearchController : Controller
    {
        private readonly ILogger<AISearchController> _logger;
        private readonly AISearch aiSearch;
        public AISearchController(ILogger<AISearchController> _logger, AISearch aiSearch)
        {
            this._logger = _logger;
            this.aiSearch = aiSearch;
        }

        [HttpGet("{blobName}")]
        public async Task<IActionResult> GetAISearch([FromRoute] string blobName)
        {
            try
            {
                var handler = new GetAISearchHandler(aiSearch);
                await handler.HandleAsync(blobName);
                return Ok();
            } 
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message);
            }  
            
        }
    }
}
