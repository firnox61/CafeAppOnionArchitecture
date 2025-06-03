using Cafe.Core.Abstractions;

namespace Cafe.Domain.Entities

{
    public class Payment : IEntity
    {
        public int Id { get; set; }

        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;

        public decimal TotalAmount { get; set; }
        public string PaymentType { get; set; } = null!; // Nakit, Kredi Kartı, vs.
        public DateTime PaidAt { get; set; }
    }
}
