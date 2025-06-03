using Cafe.Domain.Entities;

namespace Cafe.Application.Repositories
{
    public interface IPaymentDal : IEntityRepository<Payment>
    {
        Task<Payment?> GetByOrderIdAsync(int orderId);
    }
}
