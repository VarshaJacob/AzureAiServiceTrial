using Azure.Search.Documents.Indexes.Models;
using Azure.Storage.Blobs;
using AzureAiServiceTrial;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.Extensions.Azure;
using static System.Net.Mime.MediaTypeNames;
using System.Text;

namespace AzureAIServiceTrialApi
{
    public class BlobAccess
    {
        private BlobAccessOptions blobAccessOptions;
        private BlobContainerClient blobContainerClient;
        public BlobAccess(BlobAccessOptions blobAccessOptions)
        {
            this.blobAccessOptions = blobAccessOptions;
            blobContainerClient = new BlobServiceClient(blobAccessOptions.ConnectionString).GetBlobContainerClient(blobAccessOptions.ContainerName);
        }

        public async Task UploadFile(IBrowserFile file)
        {
            try
            {
                var blobName = file.Name;
                var blobClient = blobContainerClient.GetBlobClient(blobName);
                await blobClient.UploadAsync(file.OpenReadStream(), true);
            }
            catch (Exception ex)
            {

            }

        }

        public async Task UploadWebScrap(string content)
        {
            try
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(content);
                var blobName = content.Substring(0,6);
                var blobClient = blobContainerClient.GetBlobClient(blobName);

                //await blobClient.UploadAsync(htmlDocument.ParsedText, true);

                using (MemoryStream stream = new MemoryStream(byteArray))
                {
                    await blobClient.UploadAsync(stream, true);
                }
            }
            catch (Exception ex)
            {

            }

        }
    }
}
