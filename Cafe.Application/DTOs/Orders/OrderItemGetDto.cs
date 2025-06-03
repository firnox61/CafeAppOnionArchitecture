using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Orders
{
    public class OrderItemGetDto : IDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = null!;
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
