using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Orders
{
    public class OrderItemsCreateDto : IDto
    {
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
