using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Orders
{
    public class OrderCreateDto : IDto
    {
        public int TableId { get; set; }
        public List<OrderItemsCreateDto> Items { get; set; } = new();
    }
}
