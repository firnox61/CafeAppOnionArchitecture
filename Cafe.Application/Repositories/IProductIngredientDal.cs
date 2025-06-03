using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Domain.Entities;

namespace Cafe.Application.Repositories
{
    public interface IProductIngredientDal : IEntityRepository<ProductIngredient>
    {
        // Task<List<ProductIngredient>> GetAllWithIncludesAsync();
        Task<List<ProductIngredientGetDto>> GetAllWithDetailsAsync();
        Task<ProductIngredientGetDto?> GetByIdWithDetailsAsync(int ingredientId);
        Task<List<ProductIngredient>> GetAllWithProductAndIngredientAsync();
        Task<List<ProductIngredient>> GetAllWithIngredientAsync(int productId);
    }
}
