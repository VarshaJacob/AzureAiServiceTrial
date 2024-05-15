using Microsoft.AspNetCore.Mvc;

namespace AzureAIServiceTrialApi.Application
{
    public class GetAISearchHandler
    {
        private readonly AISearch aiSearch;
        public GetAISearchHandler(AISearch aiSearch)
        {
            this.aiSearch = aiSearch;
        }
        public async Task HandleAsync(string blobName)
        {
            await aiSearch.CreateIndex(blobName);
        }
    }
}
