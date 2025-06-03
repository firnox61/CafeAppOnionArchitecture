using Cafe.Application.DTOs.Ingredients;
using Cafe.Domain.Entities;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IIngredientService
    {
        Task<IDataResult<List<IngredientDto>>> GetAllAsync();
        Task<IDataResult<Ingredient?>> GetById(int id);
        Task<IResult> Add(IngredientCreateDto ingredientCreateDto);
        Task<IResult> IncreaseStockAsync(int ingredientId, int quantityToAdd);
        Task<IResult> Update(IngredientUpdateDto ingredientUpdateDto);
        Task<IResult> Delete(int ingredientId);
        Task<IDataResult<List<StockAlertDto>>> GetCriticalStockIngredientsAsync();
        Task<IDataResult<List<IngredientDto>>> SearchIngredientsAsync(string keyword);
    }
}
