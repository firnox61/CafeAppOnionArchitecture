using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.ProductIngredients
{
    public class ProductIngredientCreateDto : IDto
    {
        // public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public double QuantityRequired { get; set; }
    }
}
