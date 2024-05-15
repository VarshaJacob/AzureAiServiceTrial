using Azure.AI.OpenAI;
using Azure;
using AzureAiServiceTrial;
using HtmlAgilityPack;
using System.Text;
using AzureAIServiceTrialApi;

namespace ChatUiTrial
{
    public class HTMLAgilityWebScraper
    {
        private readonly AIService aIService;
        private readonly BlobAccess blobAccess;
        private const string question = "from the text provided, give the ingredients of coke";

        public HTMLAgilityWebScraper(AIService aIService, BlobAccess blobAccess)
        {
             this.aIService = aIService;
            this.blobAccess = blobAccess;
        }

        public async Task<string> ScrapDocument(string url)
        {
            try
            {

                // Create HtmlWeb instance
                HtmlWeb web = new HtmlWeb();

                // Load the webpage
                HtmlDocument doc = web.Load(url);

                // Specify XPath for the main content area
                string mainContentXPath = "//div[@id='mw-content-text']//p"; // XPath for paragraphs within the main content area

                // Select the main content paragraphs
                HtmlNodeCollection mainContentNodes = doc.DocumentNode.SelectNodes(mainContentXPath);

                // Check if main content paragraphs are found
                StringBuilder contentBuilder = new StringBuilder();
                if (mainContentNodes != null)
                {
                    // Create a StringBuilder to store the content
                    

                    // Loop through the main content paragraphs and append them to the StringBuilder
                    foreach (HtmlNode node in mainContentNodes)
                    {
                        // Append the inner text of each paragraph to the StringBuilder
                        contentBuilder.AppendLine(node.InnerText.Trim());
                    }
                    

                }

                await blobAccess.UploadWebScrap(contentBuilder.ToString());

                var response = await aIService.ChatWithScrape(question, contentBuilder.ToString());
                return response;
            }
            catch (Exception ex)
            {
                return string.Empty;
            }
           
        }

        public void StartScrap()
        {
            try
            {
                var doc = GetDocument();

                //HtmlNodeCollection names = doc.DocumentNode.SelectNodes("//a/h2");
               // HtmlNodeCollection prices = doc.DocumentNode.SelectNodes("//div/main/ul/li/a/span");

                //for (int i = 0; i < names.Count(); i++)
                //{
                //    Console.WriteLine("Name: {0}, Price: {1}", names[i].InnerText, prices[i].InnerText);
                //}
            }
            catch (Exception e)
            {

            }
            

        }

    }
}
