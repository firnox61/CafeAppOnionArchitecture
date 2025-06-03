using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfUserDal : EfEntityRepositoryBase<User, DataContext>, IUserDal
    {

        public EfUserDal(DataContext context) : base(context) { }

        public async Task<List<OperationClaim>> GetClaimsAsync(User user)
        {
            var claims = await _context.UserOperationClaims
                .Where(uoc => uoc.UserId == user.Id)
                .Include(uoc => uoc.OperationClaim)
                .Select(uoc => uoc.OperationClaim)
                .ToListAsync();

            return claims;
        }
    }
}
