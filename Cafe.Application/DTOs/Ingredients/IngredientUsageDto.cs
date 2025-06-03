using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Ingredients
{
    public class IngredientUsageDto : IDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = string.Empty;
        public double QuantityRequired { get; set; }
    }
}
