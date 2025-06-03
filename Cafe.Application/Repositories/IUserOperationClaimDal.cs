using Cafe.Domain.Entities;

namespace Cafe.Application.Repositories
{
    public interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {
        Task<List<UserOperationClaim>> GetWithDetailsByUserIdAsync(int userId);
    }
}
