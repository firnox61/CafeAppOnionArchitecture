using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Products
{
    public class GroupedProductIngredientsDto : IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public List<ProductIngredientDto> Ingredients { get; set; } = new();
    }
}
