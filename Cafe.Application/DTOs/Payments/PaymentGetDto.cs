using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Payments
{
    public class PaymentGetDto : IDto
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
        public string PaymentType { get; set; } = string.Empty;
        public DateTime PaidAt { get; set; }
    }
}
