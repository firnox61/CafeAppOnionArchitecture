using Cafe.Application.DTOs.Categories;
using Cafe.Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface ICategoryService
    {
        Task<IDataResult<List<CategoryGetDto>>> GetAllAsync();
        Task<IDataResult<CategoryGetDto?>> GetByIdAsync(int id);
        Task<IResult> AddAsync(CategoryCreateDto categoryCreateDto);
        Task<IResult> UpdateAsync(CategoryUpdateDto categoryUpdateDto);
        Task<IResult> DeleteAsync(int id);
        Task<IDataResult<List<CategoryGetDto>>> SearchAsync(string keyword);
    }
}
