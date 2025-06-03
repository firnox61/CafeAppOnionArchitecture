using Cafe.Domain.Entities;

namespace Cafe.Application.Repositories
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        Task<List<Order>> GetAllWithDetailsAsync();
        Task<Order?> GetOrderWithDetailsAsync(int id);
        Task<Order?> GetWithItemsAsync(int orderId);

    }
}
