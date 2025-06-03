using Cafe.Application.DTOs.Tables;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface ITableService
    {
        Task<IDataResult<List<TableGetDto>>> GetAllAsync();
        Task<IDataResult<TableGetDto?>> GetById(int id);
        Task<IResult> Add(TableCreateDto tableCreateDto);
        Task<IResult> Update(TableUpdateDto tableUpdateDto);
        Task<IResult> Delete(int id);
        Task<IDataResult<List<TableGetDto>>> SearchTablesAsync(string keyword);
        Task<IDataResult<List<TableGetDto>>> GetEmptyTablesAsync();
    }
}
