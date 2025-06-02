using Cafe.Application.DTOs.Products;
using Cafe.Application.Utilities.Results;
using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{
    public interface IProductService
    {
        Task<IDataResult<List<ProductGetDto>>> GetAllAsync();
        Task<IDataResult<ProductGetDto?>> GetById(int id);
        Task<IResult> Add(ProductCreateDto productCreateDto);
        Task<IResult> Update(ProductUpdateDto productUpdateDto);
        Task<IResult> Delete(int productId);
        Task<IDataResult<List<ProductProductionReportDto>>> GetMostProducedProductsAsync();
        Task<IDataResult<List<ProductProductionHistoryDto>>> GetProductionHistoryReportAsync();
        Task<IResult> ProduceProduct(int productId, int quantity);
    }
}
