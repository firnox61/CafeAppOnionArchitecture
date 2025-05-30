using Cafe.Application.DTOs.Users;
using Cafe.Application.Utilities.Results;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IUserOperationClaimService
    {
        Task<IDataResult<List<UserOperationClaimListDto>>> GetByUserIdAsync(int userId);
        Task<IResult> AddAsync(UserOperationClaimCreateDto dto);
        Task<IResult> DeleteAsync(int id);
    }
}
