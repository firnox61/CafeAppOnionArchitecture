using Cafe.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Ingredients
{
    public class IngredientUsageReportDto:IDto
    {
        public string IngredientName { get; set; } = null!;
        public double TotalUsedAmount { get; set; }
    }
}
