using Cafe.Application.DTOs.Users;
using Cafe.Domain.Entities;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IUserService
    {
        Task<IDataResult<List<UserListDto>>> GetAllAsync();
        Task<IResult> EditProfil(UserDto userDto, string password);
        Task<IDataResult<UserListDto>> GetByIdAsync(int id);
        Task<IDataResult<User>> GetByEmailAsync(string email);
        Task<IDataResult<List<OperationClaim>>> GetClaimsAsync(User user);
        Task<IResult> AddAsync(UserCreateDto dto);
        Task<IResult> UpdateAsync(UserUpdateDto dto);
        Task<IResult> DeleteAsync(int id);


    }
}
