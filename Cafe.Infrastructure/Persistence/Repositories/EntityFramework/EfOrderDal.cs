using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;
using DocumentFormat.OpenXml.Office2010.Excel;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, DataContext>, IOrderDal
    {
        public EfOrderDal(DataContext context) : base(context) { }

        public async Task<List<Order>> GetAllWithDetailsAsync()
        {
            return await _context.Orders
        .Where(o => !o.IsPaid) // sadece ödenmemiş siparişler
        .Include(o => o.OrderItems)
            .ThenInclude(oi => oi.Product)
        .Include(o => o.Table) // opsiyonel
        .ToListAsync();
        }
        public async Task<Order?> GetOrderWithDetailsAsync(int id)
        {
            return await _context.Orders
         .Include(o => o.Table)
         .Include(o => o.OrderItems)
             .ThenInclude(oi => oi.Product)
         .FirstOrDefaultAsync(o => o.Id == id && !o.IsPaid);
        }
        public async Task<Order?> GetWithItemsAsync(int orderId)
        {
            return await _context.Orders
            .Include(o => o.OrderItems)
          .FirstOrDefaultAsync(o => o.Id == orderId);
        }

    }
}
