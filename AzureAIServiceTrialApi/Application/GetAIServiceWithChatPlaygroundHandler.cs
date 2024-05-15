using AzureAiServiceTrial;
using Microsoft.AspNetCore.Mvc.Diagnostics;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

namespace AzureAIServiceTrialApi.Application
{
    public class GetAIServiceWithChatPlaygroundHandler
    {
        //private AIOptions AIOptions { get; set; }
        private readonly AIService aiservice;
        public GetAIServiceWithChatPlaygroundHandler(AIService aiservice)
        {
            this.aiservice = aiservice;
        }

        public async Task<string> HandleAsync(string question)
        {

            //var result = new GetAIServiceResult();

            var answer = await aiservice.ChatWithPlayground(question);

            //var answer = "trial";

            return answer;

        }

    }

    public class GetAIServiceWithChatPlaygroundResult
    {
        public string Answer { get; set; }
        public bool success { get; set; }
    }

    public class GetAIServiceWithChatPlaygroundCommand
    {
        public string Question { get; set; }
    }
}
