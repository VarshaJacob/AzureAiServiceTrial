using Azure;
using Azure.AI.OpenAI;
using AzureAIServiceTrialApi;
using Microsoft.Extensions.Options;
using System.Text.RegularExpressions;

namespace AzureAiServiceTrial
{
    public class AIService
    {
        private AIOptions AIOptions;
        private OpenAIClient OpenAIClient;

        public AIService(AIOptions aiOptions)
        {
            AIOptions = aiOptions;
            OpenAIClient = new(new Uri(AIOptions.AzureOpenAIResourceUri), new AzureKeyCredential(AIOptions.AzueOpenAIApiKey));
        }

        public async Task<string> Chat(string question)
        {
            try
            {
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = AIOptions.AzureOpenAIDeploymentName, // Use DeploymentName for "model" with non-Azure clients
                    Messages =
                {
                    // The system message represents instructions or other guidance about how the assistant should behave
                    new ChatRequestSystemMessage("You are a helpful assistant. You will talk like a pirate."),
                    // User messages represent current or historical input from the end user
                    new ChatRequestUserMessage("Can you help me?"),
                    // Assistant messages represent historical responses from the assistant
                    new ChatRequestAssistantMessage("Arrrr! Of course, me hearty! What can I do for ye?"),
                    new ChatRequestUserMessage(question),
                }
                };

                Response<ChatCompletions> response = await OpenAIClient.GetChatCompletionsAsync(chatCompletionsOptions);
                ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
                return responseMessage.Content;
                
            }
            catch (Exception ex)
            {
                return "Excpetion occurred";
            }
            
        }

        public async Task<string> ChatWithPlayground(string question)
        {
            try
            {
                AzureSearchChatExtensionConfiguration config = new()
                {
                    SearchEndpoint = new Uri(AIOptions.AISearchEndpoint),
                    Authentication = new OnYourDataApiKeyAuthenticationOptions(AIOptions.AISearchKey),
                    IndexName = AIOptions.AISearchIndexChatPlayground,
                };

                //set up ai chat query/completeion
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = AIOptions.AzureOpenAIDeploymentName, // Use DeploymentName for "model" with non-Azure clients
                    Messages =
                {
                    // The system message represents instructions or other guidance about how the assistant should behave
                    new ChatRequestSystemMessage("You are a helpful assistant. You will talk like a pirate."),
                    // User messages represent current or historical input from the end user
                    new ChatRequestUserMessage("Can you help me?"),
                    // Assistant messages represent historical responses from the assistant
                    new ChatRequestAssistantMessage("Arrrr! Of course, me hearty! What can I do for ye?"),
                    new ChatRequestUserMessage(question),
                },
                    AzureExtensionsOptions = new AzureChatExtensionsOptions()
                    {
                        Extensions = { config }
                    }
                };

                Response<ChatCompletions> response = await OpenAIClient.GetChatCompletionsAsync(chatCompletionsOptions);
                ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
                return responseMessage.Content;
            }
            catch (Exception ex)
            {
                return "Excpetion occurred";
            }
            
        }
        public async Task<string> ChatWithData(string question)
        {
            try
            {
                AzureSearchChatExtensionConfiguration config = new()
                {
                    SearchEndpoint = new Uri(AIOptions.AISearchEndpoint),
                    Authentication = new OnYourDataApiKeyAuthenticationOptions(AIOptions.AISearchKey),
                    IndexName = AIOptions.AISearchIndex,
                };

                //set up ai chat query/completeion
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = AIOptions.AzureOpenAIDeploymentName, // Use DeploymentName for "model" with non-Azure clients
                    Messages =
                {
                    // The system message represents instructions or other guidance about how the assistant should behave
                    new ChatRequestSystemMessage("You are a helpful assistant. You will talk like a pirate."),
                    //new AzureChatExtensionDataSourceResponseCitation
                    // User messages represent current or historical input from the end user
                    new ChatRequestUserMessage("Can you help me?"),
                    // Assistant messages represent historical responses from the assistant
                    new ChatRequestAssistantMessage("Arrrr! Of course, me hearty! What can I do for ye?"),
                    new ChatRequestUserMessage(question),
                },
                    AzureExtensionsOptions = new AzureChatExtensionsOptions()
                    {
                        Extensions = { config }
                    }
                };

                Response<ChatCompletions> response = await OpenAIClient.GetChatCompletionsAsync(chatCompletionsOptions);
                ChatResponseMessage responseMessage = response.Value.Choices[0].Message;

                //var temp = O
                return responseMessage.Content;
            }
            catch (Exception ex)
            {
                return "Excpetion occurred";
            }

        }
        public async Task ParseAnswer()
        {
            var answer = "Hybrid working is a purposeful and deliberate choice about how and where we work, depending on what needs to be done. It involves splitting the working week between client/audit entity sites, the office, and home [doc1]. At KPMG, client delivery is at the heart of what they do, so client-facing teams are expected to be on client site or working together in the offices to meet the needs of clients and audited entities [doc1]. The company has previously advised that this should be a minimum of two days a week, but to deliver their best work, they should be striving to spend as much time as possible with clients [doc1].";

            var pattern = "/\\[(doc\\d\\d?\\d?)]/g";
            var rg = new Regex(pattern);

            var citationLinks = rg.Matches(answer);

            foreach (Match citationLink in citationLinks )
            {
                //var citationIndex = citationLink.Value.Substring()
            }
        }

        public async Task<string> ChatWithScrape(string question, string context)
        {
            try
            {
                var chatCompletionsOptions = new ChatCompletionsOptions()
                {
                    DeploymentName = AIOptions.AzureOpenAIDeploymentName, // Use DeploymentName for "model" with non-Azure clients
                    Messages =
                {
                    // The system message represents instructions or other guidance about how the assistant should behave
                    new ChatRequestSystemMessage($"You are a helpful assistant. You will use this {context} as source"),
                    // User messages represent current or historical input from the end user
                    new ChatRequestUserMessage("Can you help me?"),
                    // Assistant messages represent historical responses from the assistant
                    new ChatRequestAssistantMessage("Arrrr! Of course, me hearty! What can I do for ye?"),
                    new ChatRequestUserMessage(question),
                }
                };

                Response<ChatCompletions> response = await OpenAIClient.GetChatCompletionsAsync(chatCompletionsOptions);
                ChatResponseMessage responseMessage = response.Value.Choices[0].Message;
                return responseMessage.Content;

            }
            catch (Exception ex)
            {
                return "Excpetion occurred";
            }

        }


    }

   
}
