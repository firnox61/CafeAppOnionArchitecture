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
    public interface IOrderItemService
    {
        Task<IDataResult<List<OrderItem>>> GetAllAsync();
        Task<IDataResult<OrderItem?>> GetById(int id);
        Task<IResult> Add(OrderItemsCreateDto orderItemsCreateDto);
        Task<IResult> Update(OrderItem orderItem);
        Task<IResult> Delete(OrderItem orderItem);
    }
}
