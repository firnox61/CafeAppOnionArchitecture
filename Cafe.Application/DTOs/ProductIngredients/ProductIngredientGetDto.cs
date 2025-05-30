using Cafe.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.ProductIngredients
{
    public class ProductIngredientGetDto : IDto
    {
        public int ProductId { get; set; }
        public int IngredientId { get; set; }
        public string IngredientName { get; set; } = null!;
        public double QuantityRequired { get; set; }
    }
}
