using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Ingredients
{
    public class IngredientUsageReportDto:IDto
    {
        public string IngredientName { get; set; } = null!;
        public double TotalUsedAmount { get; set; }
    }
}
