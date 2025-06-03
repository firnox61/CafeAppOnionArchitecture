using Cafe.Application.Interfaces.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Services.Managers
{
    public class FileServiceManager : IFileService
    {
        public async Task<string?> UploadImageAsync(IFormFile? file, string folder)
        {
            if (file == null)
                return null;

            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder, fileName);
            Directory.CreateDirectory(Path.GetDirectoryName(path)!);

            using var stream = new FileStream(path, FileMode.Create);
            await file.CopyToAsync(stream);

            return fileName;
        }
        public async Task DeleteImageAsync(string folder, string fileName)
        {
            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folder, fileName);
            if (File.Exists(path))
            {
                await Task.Run(() => File.Delete(path)); // async olmayan API'yi async gibi çalıştırma
            }
        }
    }
}
