using Microsoft.AspNetCore.Http;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IFileService
    {
        Task<string?> UploadImageAsync(IFormFile? file, string folder);
        Task DeleteImageAsync(string folder, string fileName);
    }
}
