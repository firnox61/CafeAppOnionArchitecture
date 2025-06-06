using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Core.Abstractions;
using Microsoft.AspNetCore.Http;

//using Microsoft.AspNetCore.Http;

namespace Cafe.Application.DTOs.Products
{
    public class ProductUpdateDto : IDto
    {
        public int Id { get; set; } // Güncellenecek ürün ID'si
        public int CategoryId { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public IFormFile? Image { get; set; }

        // Opsiyonel olarak malzeme güncellemesi de desteklenebilir
        public List<ProductIngredientCreateDto>? ProductIngredients { get; set; } = new();
    }

}
