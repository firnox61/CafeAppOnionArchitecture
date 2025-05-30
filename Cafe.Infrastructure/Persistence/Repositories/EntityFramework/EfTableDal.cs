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
    public class EfTableDal : EfEntityRepositoryBase<Table, DataContext>, ITableDal
    {
        public EfTableDal(DataContext context) : base(context) { }
        public async Task<List<Table>> GetAllWithOrdersAsync()
        {
            return await _context.Tables
                .Include(t => t.Orders)
                    .ThenInclude(o => o.OrderItems)
                .ToListAsync();
        }

        public async Task<Table?> GetWithOrdersByIdAsync(int id)
        {
            return await _context.Tables
                .Include(t => t.Orders)
                    .ThenInclude(o => o.OrderItems)
                .FirstOrDefaultAsync(t => t.Id == id);
        }

    }
}
