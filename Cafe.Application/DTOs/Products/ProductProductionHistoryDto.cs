using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Products
{
    public class ProductProductionHistoryDto:IDto
    {
        public string ProductName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public int TotalProduced { get; set; }
        public DateTime LastProducedAt { get; set; }
    }

}
