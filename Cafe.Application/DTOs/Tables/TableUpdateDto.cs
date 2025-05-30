using Cafe.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Tables
{
    public class TableUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // Örn: Masa 1
    }
}
