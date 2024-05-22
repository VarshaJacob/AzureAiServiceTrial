using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionApp1
{
    public static class BlobTrigger
    {
        [FunctionName("BlobtriggerFunction")]
        public static async Task Run (
            [BlobTrigger("fileupload-trial/{fileName}", Connection = "AzureWebJobsStorage")] Stream myBlob,
            string fileName,
            [DurableClient] IDurableOrchestrationClient starter,
            ILogger log
            )
        {
            log.LogInformation($"C# Blob trigger function Processed blob\n Name: {fileName} \n Size: {myBlob.Length} Bytes");

            // Start the orchestration
            string instanceId = await starter.StartNewAsync("BlobTriggerOrchestratorFunction", fileName);

            log.LogInformation($"Started orchestration with ID = '{instanceId}'.");
        }

        [FunctionName("BlobTriggerOrchestratorFunction")]
        public static async Task RunOrchestrator(
        [OrchestrationTrigger] IDurableOrchestrationContext context)
        {
            var blobName = context.InstanceId;

            await context.CallActivityAsync("BlobTriggerActivityFunction", blobName);
        }

        [FunctionName("BlobTriggerActivityFunction")]
        public static void ProcessBlob([ActivityTrigger] string blobName, ILogger log)
        {
            log.LogInformation($"Processing blob: {blobName}");
            // the logic
        }
    }
}
