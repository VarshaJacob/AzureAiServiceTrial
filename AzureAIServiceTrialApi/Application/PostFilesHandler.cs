using Microsoft.AspNetCore.Components.Forms;

namespace AzureAIServiceTrialApi.Application
{
    public class PostFilesHandler
    {
        private readonly BlobAccess blobAccess;
        public PostFilesHandler(BlobAccess blobAccess)
        {
            this.blobAccess = blobAccess;
        }

        public async Task HandleAsync(IBrowserFile file)
        {
            await blobAccess.UploadFile(file);

        }
    }
}
