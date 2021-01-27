using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Services.BlobStorageService.Configurations;
using Services.BlobStorageService.Utilities;

namespace Services.BlobStorageService
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".png", ".jpg" };
        
        public BlobService(AzureBlobStorageConfig config)
        {
            _fileSizeLimit = config.FileSizeLimit;  
            _blobServiceClient = new BlobServiceClient(config.ConnectionString);   
            _containerClient = _blobServiceClient.GetBlobContainerClient(config.Container);
        }

        public async Task<string> UploadContentBlobAsync(IFormFile file, ModelStateDictionary modelState)
        {
            var trustedFileNameForFileStorage = Path.GetRandomFileName();
            var blobClient = _containerClient.GetBlobClient(trustedFileNameForFileStorage);
            var formFileContent = await FileHelpers.ProcessFormFile(file, modelState, _permittedExtensions, _fileSizeLimit);

            await using var memoryStream = new MemoryStream(formFileContent);
            await blobClient.UploadAsync(memoryStream, new BlobHttpHeaders {ContentType = file.ContentType});

            return blobClient.Uri.AbsoluteUri;
        }

        public async Task DeleteBlobAsync(string blobName)
        {
            var blobClient = _containerClient.GetBlobClient(blobName);
            await blobClient.DeleteIfExistsAsync();
        }
    }
}