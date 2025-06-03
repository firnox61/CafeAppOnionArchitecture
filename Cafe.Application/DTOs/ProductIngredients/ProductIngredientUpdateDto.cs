using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.ProductIngredients
{
    public class ProductIngredientUpdateDto : IDto
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public double QuantityRequired { get; set; } // güncellenen miktar
    }
}
