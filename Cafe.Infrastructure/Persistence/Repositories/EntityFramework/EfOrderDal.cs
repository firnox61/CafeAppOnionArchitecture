using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, DataContext>, IOrderDal
    {
        public EfOrderDal(DataContext context) : base(context) { }

        public async Task<List<Order>> GetAllWithDetailsAsync()
        {
            return await _context.Orders
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
                    .ThenInclude(oi => oi.Product) // ✅ ürün bilgileri gelsin diye şart
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        public async Task<Order?> GetWithItemsAsync(int orderId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);
        }

    }
}
