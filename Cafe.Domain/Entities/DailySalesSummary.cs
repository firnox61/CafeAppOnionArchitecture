using Cafe.Core.Abstractions;

namespace Cafe.Domain.Entities
{
    public class DailySalesSummary : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } // Sadece tarih kısmı
        public decimal TotalAmount { get; set; } // Günlük toplam ciro
    }
}
