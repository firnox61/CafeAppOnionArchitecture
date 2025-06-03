using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Core.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Cafe.Application.DTOs.Products
{
    public class ProductCreateDto : IDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile? Image { get; set; }

        public List<ProductIngredientCreateDto>? ProductIngredients { get; set; } = new();
    }
}
