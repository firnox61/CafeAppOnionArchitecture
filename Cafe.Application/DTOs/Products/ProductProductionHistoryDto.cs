using Cafe.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Products
{
    public class ProductProductionHistoryDto:IDto
    {
        public string ProductName { get; set; } = null!;
        public int TotalProduced { get; set; }
        public DateTime LastProducedAt { get; set; }
    }

}
