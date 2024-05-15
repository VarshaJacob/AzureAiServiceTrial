using AzureAiServiceTrial;
using AzureAIServiceTrialApi.Application;
using Microsoft.AspNetCore.Mvc;

namespace AzureAIServiceTrialApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AIServiceController : ControllerBase
    {

        private readonly ILogger<AIServiceController> _logger;
        private readonly AIService aiservice;

        public AIServiceController(ILogger<AIServiceController> logger, AIService aiservice)
        {
            _logger = logger;
            this.aiservice = aiservice;
        }

        [HttpGet]
        public async Task<IActionResult> GetAIService([FromQuery] string question)
        {
            try
            {
                var handler = new GetAIServiceHandler(aiservice);
                var result = await handler.HandleAsync(question);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("chatplayground")]
        public async Task<IActionResult> GetAIServiceWithChatPlayground([FromQuery] string question)
        {
            try
            {
                var handler = new GetAIServiceWithChatPlaygroundHandler(aiservice);
                var result = await handler.HandleAsync(question);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet("owndata")]
        public async Task<IActionResult> GetAIServiceWithData([FromQuery] string question)
        {
            try
            {
                var handler = new GetAIServiceWithDataHandler(aiservice);
                var result = await handler.HandleAsync(question);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
