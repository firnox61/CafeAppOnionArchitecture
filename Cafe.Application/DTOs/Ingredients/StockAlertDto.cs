using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Ingredients
{
    public class StockAlertDto : IDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public double Stock { get; set; }
        public double MinStockThreshold { get; set; }
        public bool IsCritical => Stock <= MinStockThreshold;
    }
}
