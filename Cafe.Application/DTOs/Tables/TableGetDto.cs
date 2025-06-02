using Cafe.Application.Abstraction;
using Cafe.Application.DTOs.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Tables
{
    public class TableGetDto : IDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<OrderSummaryDto> ActiveOrders { get; set; } = new(); // sadece unpaid orders
        //public List<OrderGetDto> ActiveOrders { get; set; } = new();

    }
}
