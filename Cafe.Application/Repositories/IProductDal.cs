using Cafe.Application.DTOs.Products;
using Cafe.Domain.Entities;
using Entities.DTOs;
using Entities.DTOs.Products;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Repositories
{
    public interface IProductDal : IEntityRepository<Product>
    {
        //Task AddAsync(ProductCreateDto productCreateDto);
        Task<List<ProductGetDto>> GetProductDetailsAsync();

    }
}
