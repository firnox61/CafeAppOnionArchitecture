using Cafe.Core.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Categories
{
    public class CategoryCreateDto:IDto
    {
        public string Name { get; set; } = null!;
    }
}
