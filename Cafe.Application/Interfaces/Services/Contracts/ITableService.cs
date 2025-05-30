using Cafe.Application.DTOs.Tables;
using Cafe.Application.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface ITableService
    {
        Task<IDataResult<List<TableGetDto>>> GetAllAsync();
        Task<IDataResult<TableGetDto?>> GetById(int id);
        Task<IResult> Add(TableCreateDto tableCreateDto);
        Task<IResult> Update(TableUpdateDto tableUpdateDto);
        Task<IResult> Delete(int id);
    }
}
