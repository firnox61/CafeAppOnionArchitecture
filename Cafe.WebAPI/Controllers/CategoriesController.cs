using Cafe.Application.DTOs.Categories;
using Cafe.Application.Interfaces.Services.Contracts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }



        // GET: api/category
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _categoryService.GetAllAsync();
            return StatusCode(result.Success ? 200 : 400, result);
        }

        // GET: api/category/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _categoryService.GetByIdAsync(id);
            return StatusCode(result.Success ? 200 : 404, result);
        }

        // POST: api/category
        [HttpPost]
        public async Task<IActionResult> Add([FromForm] CategoryCreateDto dto)
        {
            var result = await _categoryService.AddAsync(dto);
            return StatusCode(result.Success ? 201 : 400, result);
        }

        // PUT: api/category
        [HttpPut]
        public async Task<IActionResult> Update([FromForm] CategoryUpdateDto dto)
        {
            var result = await _categoryService.UpdateAsync(dto);
            return StatusCode(result.Success ? 200 : 400, result);
        }

        // DELETE: api/category/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _categoryService.DeleteAsync(id);
            return StatusCode(result.Success ? 200 : 400, result);
        }

        // GET: api/category/search?keyword=tatlı
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] string keyword)
        {
            var result = await _categoryService.SearchAsync(keyword);
            return StatusCode(result.Success ? 200 : 400, result);
        }
    }
}
