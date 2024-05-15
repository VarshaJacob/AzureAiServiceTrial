using Azure;
using AzureAIServiceTrialApi;

namespace AzureAiServiceTrial
{
    public class AIOptions
    {
        public string AzureOpenAIResourceUri { get; set; }
        public string AzueOpenAIApiKey { get; set; }
        public string AzureOpenAIDeploymentName { get; set; }

        public string AISearchEndpoint { get; set; }
        public string AISearchKey { get; set; }
        public string AISearchIndexer { get; set; }
        public string AISearchIndex { get; set; }
        public string AISearchIndexChatPlayground { get; set; }
        public string AIDataSource { get; set; }
        public string AISkillSet { get; set; }

    }
}
