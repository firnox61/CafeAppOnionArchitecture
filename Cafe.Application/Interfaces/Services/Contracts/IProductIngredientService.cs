using Cafe.Application.DTOs.Ingredients;
using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Application.DTOs.Products;
using Cafe.Application.DTOs.Users;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IProductIngredientService
    {
        Task<IDataResult<List<GroupedProductIngredientsDto>>> GetAllAsync();
        Task<IDataResult<ProductIngredientGetDto>> GetById(int id);
        Task<IResult> Add(ProductIngredientCreateDto productIngredientCreateDto);
        Task<IResult> UpdateAsync(ProductIngredientUpdateDto dto);
        Task<IResult> DeleteAsync(int productId, int ingredientId);
        Task<IDataResult<List<IngredientUsageReportDto>>> GetMostUsedIngredientsAsync();
        Task<IDataResult<List<IngredientUsageReportDto>>> GetMostUsedIngredientsAsync(UsageReportFilterDto filter);

    }
}
