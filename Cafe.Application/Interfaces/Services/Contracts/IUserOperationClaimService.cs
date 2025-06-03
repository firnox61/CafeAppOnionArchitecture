using Cafe.Application.DTOs.Users;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IUserOperationClaimService
    {
        Task<IDataResult<List<UserOperationClaimListDto>>> GetByUserIdAsync(int userId);
        Task<IResult> AddAsync(UserOperationClaimCreateDto dto);
        Task<IResult> DeleteAsync(int id);
    }
}
