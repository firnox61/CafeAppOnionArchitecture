using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Orders
{
    public class OrderUpdateDto : IDto
    {
        public int Id { get; set; }
        public List<OrderItemsCreateDto> Items { get; set; } = new();
    }
}
