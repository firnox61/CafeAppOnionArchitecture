using Cafe.Application.DTOs.Orders;
using Cafe.Domain.Entities;
using Cafe.Core.Utilities.Results;

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
