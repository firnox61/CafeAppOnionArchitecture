
using Cafe.Application.DTOs.Ingredients;
using Cafe.Application.Interfaces.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }
         [HttpPost("add")]
         public async Task<IActionResult> Add(IngredientCreateDto ingredientCreateDto)
         {
           
             var result = await _ingredientService.Add(ingredientCreateDto);
             if (result.Success)
             {
                 return Ok(result);
             }
             return BadRequest(result);
         }

        [HttpPost("increase-stock")]
        public async Task<IActionResult> IncreaseStock(int id, int stock)
        {
            var result = await _ingredientService.IncreaseStockAsync(id, stock);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update(IngredientUpdateDto ingredientUpdateDto)
        {
            var result = await _ingredientService.Update(ingredientUpdateDto);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int ingredient)
        {
            var result = await _ingredientService.Delete(ingredient);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ingredientService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _ingredientService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("critical-stocks")]
        public async Task<IActionResult> GetCriticalStockIngredients()
        {
            var result = await _ingredientService.GetCriticalStockIngredientsAsync();
            if (result.Success)
                return Ok(result);
            return BadRequest(result);
        }
        [HttpGet("search")]
        public async Task<IActionResult> SearchIngredients([FromQuery] string keyword)
        {
            var result = await _ingredientService.SearchIngredientsAsync(keyword);
            if (!result.Success)
                return BadRequest(result);

            return Ok(result);
        }
    }
}
