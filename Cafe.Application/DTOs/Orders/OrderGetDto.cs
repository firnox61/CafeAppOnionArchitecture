using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Orders
{
    public class OrderGetDto : IDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }
        public List<OrderItemGetDto> Items { get; set; } = new();
        public decimal TotalAmount => Items.Sum(i => i.UnitPrice * i.Quantity);
    }
}
