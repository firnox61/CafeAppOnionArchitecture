using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.ProductIngredients
{
    public class ProductIngredientDto : IDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public double QuantityRequired { get; set; }
    }
}
