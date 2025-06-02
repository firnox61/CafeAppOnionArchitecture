using Cafe.Application.DTOs.Orders;
using Cafe.Application.Utilities.Results;
using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IOrderService
    {
        Task<IDataResult<List<OrderGetDto>>> GetAllAsync();
        Task<IDataResult<OrderGetDto>> GetById(int id);
        Task<IResult> Add(OrderCreateDto orderCreateDto);
        Task<IResult> Update(OrderUpdateDto orderUpdateDto);
        Task<IResult> Delete(Order order);
        Task<IResult> DeleteOrderAsync(int orderId);
    }
}
