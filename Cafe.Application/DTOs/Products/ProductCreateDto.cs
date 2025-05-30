using Cafe.Application.Abstraction;
using Cafe.Application.DTOs.ProductIngredients;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
