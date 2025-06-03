

//using Entities.Concrete;
using Cafe.Domain.Entities;

namespace Cafe.Application.Repositories
{
    public interface IUserDal : IEntityRepository<User>
    {
        Task<List<OperationClaim>> GetClaimsAsync(User user);
    }
}
