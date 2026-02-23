using System.ComponentModel.DataAnnotations;

namespace RestaurantManager.Dishes
{
    using Context;
    using Microsoft.AspNetCore.Http;
    using Repositories;
    using RestaurantManager.Services;

    public interface IDishService
    {
        Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryUpdateDish(Guid DishId, Dish updateDish);
        Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryAddImage(Guid dishId, IFormFile file);
        Task<((byte[] Image, string ContentType)?, IEnumerable<ValidationResult> errors)> TryGetDishImageById(Guid dishId);
        Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryRemoveImage(Guid dishId);
        Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryRemoveDish(Guid DishId);
    }

    public class DishService : IDishService
    {
        private readonly IDishRepository _dishRepository;
        private readonly IImageService _imageService;
        private readonly IUnitOfWork<RestaurantManagerContext> _unitOfWork;

        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        public DishService(
            IDishRepository dishRepository,
            IImageService imageService,
            IUnitOfWork<RestaurantManagerContext> unitOfWork
            )
        {
            _dishRepository = dishRepository;
            _imageService = imageService;
            _unitOfWork = unitOfWork;
        }

        public async Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryUpdateDish(Guid dishId, Dish updateDish)
        {
            var dish = await _dishRepository.GetDishById(dishId);
            if (dish == null)
                return (null, []);

            dish.Name = updateDish.Name;
            dish.Order = updateDish.Order;
            dish.Description = updateDish.Description;
            dish.Prize = updateDish.Prize;
            dish.Allergens = updateDish.Allergens;

            await _unitOfWork.CompleteAsync();

            return (dish, []);
        }

        public async Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryAddImage(Guid dishId, IFormFile file)
        {
            var dish = await _dishRepository.GetDishById(dishId);
            if (dish == null)
                return (null, []);

            var fileName = await _imageService.SaveDishImageAsync(file);
            if (fileName == null)
            {
                return (dish, [new ValidationResult("filename null after upload file.")]);
            }

            dish.ImageName = fileName;

            await _unitOfWork.CompleteAsync();

            return (dish, []);
        }

        public async Task<( (byte[] Image, string ContentType)? , IEnumerable<ValidationResult> errors)> TryGetDishImageById(Guid dishId)
        {
            var dish = await _dishRepository.GetDishById(dishId);
            if (dish == null)
                return (null, []);

            if (string.IsNullOrEmpty(dish.ImageName))
                return (null, []);

            var imageBytes = await _imageService.GetDishImageAsync(dish.ImageName);

            return ((imageBytes, GetContentType(dish.ImageName)), []);
        }

        public async Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryRemoveImage(Guid dishId)
        {
            var dish = await _dishRepository.GetDishById(dishId);
            if (dish == null)
                return (null, []);

            if (dish.ImageName == null)
                return (dish, [new ValidationResult($"Dish ({dishId}) hasn't image.")]);

            if (await _imageService.DeleteDishImageAsync(dish.ImageName) == false)
            {
                return (dish, [new ValidationResult("Any file was deleted.")]);
            }

            dish.ImageName = null;

            await _unitOfWork.CompleteAsync();

            return (dish, []);
        }

        public async Task<(Dish? Dish, IEnumerable<ValidationResult> errors)> TryRemoveDish(Guid dishId)
        {
            var dish = await _dishRepository.GetDishById(dishId);
            if (dish == null)
                return (null, []);

            _dishRepository.RemoveDish(dish);

            await _unitOfWork.CompleteAsync();

            return (dish, []);
        }


        private string GetContentType(string fileName)
        {
            var extension = Path.GetExtension(fileName).ToLowerInvariant();

            return extension switch
            {
                ".jpg" or ".jpeg" => "image/jpeg",
                ".png" => "image/png",
                ".gif" => "image/gif",
                ".webp" => "image/webp",
                _ => "application/octet-stream"
            };
        }
    }
}
