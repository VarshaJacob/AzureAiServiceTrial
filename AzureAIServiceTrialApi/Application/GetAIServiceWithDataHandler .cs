using AzureAiServiceTrial;

namespace AzureAIServiceTrialApi.Application
{
    public class GetAIServiceWithDataHandler
    {
        //private AIOptions AIOptions { get; set; }
        private readonly AIService aiservice;
        public GetAIServiceWithDataHandler(AIService aiservice)
        {
            this.aiservice = aiservice;
        }

        public async Task<string> HandleAsync(string question)
        {

            //var result = new GetAIServiceResult();

            var answer = await aiservice.ChatWithData(question);

            //var answer = "trial";

            return answer;

        }

    }

    public class GetAIServiceWithDataResult
    {
        public string Answer { get; set; }
        public bool success { get; set; }
    }

    public class GetAIServiceWithDataCommand
    {
        public string Question { get; set; }
    }
}
