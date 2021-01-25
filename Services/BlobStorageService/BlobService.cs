using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Options;
using Services.BlobStorageService.Options;
using Services.BlobStorageService.Utilities;
using BlobInfo = Entities.Models.BlobInfo;

namespace Services.BlobStorageService
{
    public class BlobService : IBlobService
    {
        private readonly BlobServiceClient _blobServiceClient;
        private readonly BlobContainerClient _containerClient;
        private readonly AzureBlobStorageOption _options;
        private readonly long _fileSizeLimit;
        private readonly string[] _permittedExtensions = { ".png", ".jpg" };
        
        public BlobService(IOptions<AzureBlobStorageOption> options)
        {
            _options = options.Value;

            _fileSizeLimit = _options.FileSizeLimit;  
            _blobServiceClient = new BlobServiceClient(_options.ConnectionString);   
            _containerClient = _blobServiceClient.GetBlobContainerClient(_options.Container);
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