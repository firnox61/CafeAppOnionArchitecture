
using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfPaymentDal : EfEntityRepositoryBase<Payment, DataContext>, IPaymentDal
    {
        public EfPaymentDal(DataContext context) : base(context) { }
        public async Task<Payment?> GetByOrderIdAsync(int orderId)
        {
            return await _context.Payments
                .Include(p => p.Order)
                .FirstOrDefaultAsync(p => p.OrderId == orderId);
        }
    }
}
