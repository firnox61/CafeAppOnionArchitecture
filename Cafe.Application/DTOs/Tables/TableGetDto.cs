using Cafe.Application.DTOs.Orders;
using Cafe.Core.Abstractions;

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
