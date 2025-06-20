﻿using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, DataContext>, IUserOperationClaimDal
    {
        public EfUserOperationClaimDal(DataContext context) : base(context) { }

        public async Task<List<UserOperationClaim>> GetWithDetailsByUserIdAsync(int userId)
        {
            return await _context.UserOperationClaims
                .Include(x => x.User)
                .Include(x => x.OperationClaim)
                .Where(x => x.UserId == userId)
                .ToListAsync();
        }
    }
}
