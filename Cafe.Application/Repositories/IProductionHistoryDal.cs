using Cafe.Domain.Entities;

namespace Cafe.Application.Repositories
{
    public interface IProductionHistoryDal : IEntityRepository<ProductionHistory>
    {
        Task<List<ProductionHistory>> GetAllWithProductAsync();
    }
}
