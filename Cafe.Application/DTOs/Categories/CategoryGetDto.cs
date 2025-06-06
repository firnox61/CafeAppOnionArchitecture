using Cafe.Application.DTOs.Products;
using Cafe.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Categories
{
    public class CategoryGetDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<ProductGetDto>? Products { get; set; } = new(); // opsiyonel
    }
}
