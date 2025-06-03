using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, DataContext>, IOperationClaimDal
    {
        public EfOperationClaimDal(DataContext context) : base(context) { }
    }
}
