using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfOrderItemDal : EfEntityRepositoryBase<OrderItem, DataContext>, IOrderItemDal
    {
        public EfOrderItemDal(DataContext context) : base(context) { }
    }
}
