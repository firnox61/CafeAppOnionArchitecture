using Cafe.Application.Repositories;
using Cafe.Domain.Entities;
using Cafe.Infrastructure.Persistence.Context;

namespace Cafe.Infrastructure.Persistence.Repositories.EntityFramework
{
    public class EfIngredientDal : EfEntityRepositoryBase<Ingredient, DataContext>, IIngredientDal
    {
        public EfIngredientDal(DataContext context) : base(context) { }
    }
}
