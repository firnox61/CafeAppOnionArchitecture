using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Orders
{
    public class OrderSummaryDto : IDto
    {
        public int OrderId { get; set; }
        public DateTime CreatedAt { get; set; }
        public decimal TotalAmount { get; set; }
    }
}
