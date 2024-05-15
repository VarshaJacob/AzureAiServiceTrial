using AzureAiServiceTrial;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace AzureAIServiceTrialApi.Application
{
    public class GetAIServiceHandler
    {
        //private AIOptions AIOptions { get; set; }
        private readonly AIService aiservice;
        public GetAIServiceHandler(AIService aiservice)
        {
            this.aiservice = aiservice;
        }

        public async Task<string> HandleAsync(string question)
        {

            //var result = new GetAIServiceResult();

            var answer = await aiservice.Chat(question);

            //var answer = "trial";

            return answer;

        }

    }

    public class GetAIServiceResult
    {
        public string Answer { get; set; }
        public bool success { get; set; }
    }

    public class GetAIServiceCommand
    {
        public string Question { get; set; }
    }
}
