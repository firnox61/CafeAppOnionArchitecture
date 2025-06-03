using Cafe.Application.DTOs.Products;
using Cafe.Domain.Entities;

namespace Cafe.Application.Repositories
{
    public interface IProductDal : IEntityRepository<Product>
    {
        //Task AddAsync(ProductCreateDto productCreateDto);
        Task<List<ProductGetDto>> GetProductDetailsAsync();

    }
}
