using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Application.DTOs.Products;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IProductService
    {
        Task<IDataResult<List<ProductGetDto>>> GetAllAsync();
        Task<IDataResult<ProductGetDto?>> GetById(int id);
        Task<IResult> Add(ProductCreateDto productCreateDto);
        Task<IResult> Update(ProductUpdateDto productUpdateDto);
        Task<IResult> Delete(int productId);
        Task<IDataResult<List<ProductProductionReportDto>>> GetMostProducedProductsAsync();
        Task<IDataResult<List<ProductProductionHistoryDto>>> GetProductionHistoryReportAsync();
        Task<IResult> ProduceProduct(int productId, int quantity);
        Task<IDataResult<List<ProductGetDto>>> SearchProductsAsync(string keyword);
        Task<IDataResult<List<ProductIngredientDto>>> GetProductRecipeAsync(int productId);
    }
}
