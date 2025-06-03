using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Ingredients
{
    public class IngredientDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Unit { get; set; }
        public decimal Stock { get; set; }
        public decimal MinStockThreshold { get; set; }
    }
}
