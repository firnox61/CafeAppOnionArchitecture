using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfOrderItemDal : EfEntityRepositoryBase<OrderItem, DataContext>, IOrderItemDal
    {
        public EfOrderItemDal(DataContext context) : base(context) { }
    }
}
