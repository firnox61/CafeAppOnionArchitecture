using Cafe.Application.DTOs.Users;
using Cafe.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IOperationClaimService
    {
        Task<IDataResult<List<OperationClaimListDto>>> GetAllAsync();
        Task<IDataResult<OperationClaimListDto>> GetByIdAsync(int id);
        Task<IResult> AddAsync(OperationClaimCreateDto dto);
        Task<IResult> UpdateAsync(OperationClaimUpdateDto dto); // ✅ güncel hali
        Task<IResult> DeleteAsync(int id);
    }
}
