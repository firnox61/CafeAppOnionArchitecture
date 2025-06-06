using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Repositories
{
    public interface ICategoryDal:IEntityRepository<Category>
    {
        Task<List<Category>> GetAllWithProductsAsync();
        Task<Category?> GetWithProductsByIdAsync(int id);

    }
}
