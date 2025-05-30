using Cafe.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Entities
{
    public class DailySalesSummary : IEntity
    {
        public int Id { get; set; }
        public DateTime Date { get; set; } // Sadece tarih kısmı
        public decimal TotalAmount { get; set; } // Günlük toplam ciro
    }
}
