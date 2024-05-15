using ChatUiTrial;
using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System;
using AzureAiServiceTrial;

namespace AzureAIServiceTrialApi.Application
{
    public class GetWebScraperHandler : Controller
    {
        private readonly AIService aIService;
        private readonly BlobAccess blobAccess;
        public GetWebScraperHandler(AIService aIService, BlobAccess blobAccess)
        {
            this.aIService = aIService;
            this.blobAccess = blobAccess;

        }
        public async Task HandleAsync(string url)
        {
            var webscarpper = new HTMLAgilityWebScraper(aIService, blobAccess);
            string response = await webscarpper.ScrapDocument(url);


                //webscarpper.StartScrap();
        }
    }
}
