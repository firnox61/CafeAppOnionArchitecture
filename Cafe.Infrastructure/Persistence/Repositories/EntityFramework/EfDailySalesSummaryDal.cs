using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfDailySalesSummaryDal : EfEntityRepositoryBase<DailySalesSummary, DataContext>, IDailySalesSummaryDal
    {
        public EfDailySalesSummaryDal(DataContext context) : base(context) { }
    }
}
