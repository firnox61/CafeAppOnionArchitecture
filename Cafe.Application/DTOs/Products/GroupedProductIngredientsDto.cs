using Cafe.Application.Abstraction;
using Cafe.Application.DTOs.ProductIngredients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Products
{
    public class GroupedProductIngredientsDto : IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public List<ProductIngredientDto> Ingredients { get; set; } = new();
    }
}
