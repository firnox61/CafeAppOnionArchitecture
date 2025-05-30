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
    public class EfProductionHistoryDal : EfEntityRepositoryBase<ProductionHistory, DataContext>, IProductionHistoryDal
    {

        public EfProductionHistoryDal(DataContext context) : base(context) { }


        public async Task<List<ProductionHistory>> GetAllWithProductAsync()
        {
            return await _context.ProductionHistories
                .Include(p => p.Product)
                .ToListAsync();
        }
    }
}
