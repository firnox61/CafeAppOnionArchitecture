using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Products
{
    public class ProductProductionReportDto:IDto
    {
        public string ProductName { get; set; } = null!;
        public int TotalProduced { get; set; }
    }
}
