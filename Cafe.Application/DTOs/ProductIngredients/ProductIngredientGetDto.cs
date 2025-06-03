using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.ProductIngredients
{
    public class ProductIngredientGetDto : IDto
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = null!;
        public double QuantityRequired { get; set; }
    }
}
