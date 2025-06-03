using AutoMapper;
using Cafe.Application.DTOs.Orders;
using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Services.Managers
{
    public class OrderItemManager : IOrderItemService
    {
        private readonly IOrderItemDal _orderItemDal;
        private readonly IMapper _mapper;

        public OrderItemManager(IOrderItemDal orderItemDal, IMapper mapper)
        {
            _orderItemDal = orderItemDal;
            _mapper = mapper;
        }

        public async Task<IResult> Add(OrderItemsCreateDto orderItemsCreateDto)
        {
            var newOrderItem = _mapper.Map<OrderItem>(orderItemsCreateDto);
            await _orderItemDal.AddAsync(newOrderItem);
            return new SuccessResult();
        }

        public Task<IResult> Delete(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<List<OrderItem>>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IDataResult<OrderItem?>> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IResult> Update(OrderItem orderItem)
        {
            throw new NotImplementedException();
        }
    }
}
