using Cafe.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Ingredients
{
    public class StockAlertDto : IDto
    {
        public int IngredientId { get; set; }
        public string IngredientName { get; set; }
        public double Stock { get; set; }
        public double MinStockThreshold { get; set; }
        public bool IsCritical => Stock <= MinStockThreshold;
    }
}
