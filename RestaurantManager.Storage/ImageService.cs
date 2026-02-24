using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace RestaurantManager.Services
{
    public interface IImageService
    {
        Task<byte[]> GetDishImageAsync(string fileName);
        Task<string?> SaveDishImageAsync(IFormFile file);
        Task<bool> DeleteDishImageAsync(string imageUrl);
    }

    public class ImageService : IImageService
    {
        private readonly string _imagesFolder;

        public ImageService(IWebHostEnvironment env)
        {
            _imagesFolder = Path.Combine(env.ContentRootPath, "wwwroot", "images", "dishes");

            if (!Directory.Exists(_imagesFolder))
                Directory.CreateDirectory(_imagesFolder);
        }

        public async Task<string?> SaveDishImageAsync(IFormFile file)
        {
            if (file == null || file.Length == 0) return null;

            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var filePath = Path.Combine(_imagesFolder, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public async Task<byte[]> GetDishImageAsync(string fileName)
        {
            var filePath = Path.Combine(_imagesFolder, fileName);

            if (!File.Exists(filePath))
                return Array.Empty<byte>();

            return await File.ReadAllBytesAsync(filePath);
        }

        public Task<bool> DeleteDishImageAsync(string fileName)
        {
            // Si fileName viene con la ruta completa o URL, extrae solo el nombre del archivo
            var pureFileName = Path.GetFileName(fileName);
            var filePath = Path.Combine(_imagesFolder, pureFileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}