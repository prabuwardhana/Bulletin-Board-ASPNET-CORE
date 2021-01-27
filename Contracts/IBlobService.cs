using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Contracts
{
    public interface IBlobService
    {
        Task<string> UploadContentBlobAsync(IFormFile file, ModelStateDictionary modelState);
        Task DeleteBlobAsync(string blobName);
    }
}