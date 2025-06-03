using Cafe.Core.Abstractions;

namespace Cafe.Domain.Entities
{
    public class Order : IEntity
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public Table Table { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public bool IsPaid { get; set; }

        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public Payment? Payment { get; set; }
    }
}
