using Cafe.Application.DTOs.Users;
using Cafe.Application.Utilities.Results;
using Cafe.Domain.Entities;
using Cafe.Domain.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
