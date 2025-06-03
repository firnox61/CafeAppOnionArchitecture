using Cafe.Domain.Entities;

namespace Cafe.Application.Repositories
{
    public interface ITableDal : IEntityRepository<Table>
    {
        Task<List<Table>> GetAllWithOrdersAsync();
        Task<Table?> GetWithOrdersByIdAsync(int id);

    }
}
