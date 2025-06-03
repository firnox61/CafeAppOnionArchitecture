using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Ingredients
{
    public class IngredientCreateDto : IDto
    {
        public string Name { get; set; } = null!;
        public string Unit { get; set; } = null!; // örn: gram, ml, adet
        public double Stock { get; set; }
    }
}
