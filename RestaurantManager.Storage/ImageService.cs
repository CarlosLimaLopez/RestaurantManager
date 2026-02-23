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
        public async Task<byte[]> GetDishImageAsync(string fileName)
        {
            var filePath = Path.Combine("wwwroot/images/dishes", fileName);

            if (!File.Exists(filePath))
                return Array.Empty<byte>();

            return await File.ReadAllBytesAsync(filePath);
        }

        public async Task<string?> SaveDishImageAsync(IFormFile file)
        {
            var extension = Path.GetExtension(file.FileName);
            var fileName = $"{Guid.NewGuid()}{extension}";
            var uploadsFolder = "wwwroot/images/dishes";
            var filePath = Path.Combine(uploadsFolder, fileName);

            if (File.Exists(filePath))
                File.Delete(filePath);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return fileName;
        }

        public Task<bool> DeleteDishImageAsync(string fileName)
        {
            var filePath = Path.Combine("wwwroot/images/dishes", fileName);

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
                return Task.FromResult(true);
            }

            return Task.FromResult(false);
        }
    }
}
