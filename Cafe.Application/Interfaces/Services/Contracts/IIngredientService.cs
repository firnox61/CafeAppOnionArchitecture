using Cafe.Application.DTOs.Ingredients;
using Cafe.Application.Utilities.Results;
using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IIngredientService
    {
        Task<IDataResult<List<IngredientDto>>> GetAllAsync();
        Task<IDataResult<Ingredient?>> GetById(int id);
        Task<IResult> Add(IngredientCreateDto ingredientCreateDto);
        Task<IResult> Update(IngredientUpdateDto ingredientUpdateDto);
        Task<IResult> Delete(int ingredientId);
        Task<IDataResult<List<StockAlertDto>>> GetCriticalStockIngredientsAsync();
    }
}
