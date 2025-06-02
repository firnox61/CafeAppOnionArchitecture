using Cafe.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Orders
{
    public class OrderUpdateDto : IDto
    {
        public int Id { get; set; }
        public List<OrderItemsCreateDto> Items { get; set; } = new();
    }
}
