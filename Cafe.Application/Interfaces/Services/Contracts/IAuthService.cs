using Cafe.Application.DTOs.Users;
using Cafe.Core.Utilities.Results;
using Cafe.Domain.Entities;
using Cafe.Domain.Security;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IAuthService
    {
        Task<IDataResult<User>> RegisterAsync(UserForRegisterDto registerDto, string password);
        Task<IDataResult<User>> LoginAsync(UserForLoginDto loginDto);
        Task<IResult> UserExistsAsync(string email);
        Task<IDataResult<AccessToken>> CreateAccessTokenAsync(User user);
    }
}
