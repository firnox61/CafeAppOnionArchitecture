
using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Application.DTOs.Users;
using Cafe.Application.Interfaces.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductIngredientsController : ControllerBase
    {
        private readonly IProductIngredientService _productIngredientService;

        public ProductIngredientsController(IProductIngredientService productIngredientService)
        {
            _productIngredientService = productIngredientService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add([FromBody] ProductIngredientCreateDto productIngredientCreateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Geçersiz veri.");

            var result = await _productIngredientService.Add(productIngredientCreateDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] ProductIngredientUpdateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest("Geçersiz veri.");

            var result = await _productIngredientService.UpdateAsync(dto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result.Message);
        }

        // Silme: DELETE api/productingredient?productId=1&ingredientId=2
        [HttpDelete]
        public async Task<IActionResult> Delete([FromQuery] int productId, [FromQuery] int ingredientId)
        {
            var result = await _productIngredientService.DeleteAsync(productId, ingredientId);
            if (!result.Success)
                return NotFound(result.Message);

            return Ok(result.Message);
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _productIngredientService.GetAllAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }

        [HttpGet("getbyid/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _productIngredientService.GetById(id);
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpGet("getmostusedIngredients")]
        public async Task<IActionResult> GetMostUsedIngredients()
        {
            var result = await _productIngredientService.GetMostUsedIngredientsAsync();
            if (result.Success)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        [HttpPost("getmostusedingredients-bydate")]
        public async Task<IActionResult> GetMostUsedIngredientsByDate([FromBody] UsageReportFilterDto filter)
        {
            var result = await _productIngredientService.GetMostUsedIngredientsAsync(filter);
            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
