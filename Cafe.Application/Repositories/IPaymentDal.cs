using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Repositories
{
    public interface IPaymentDal : IEntityRepository<Payment>
    {
        Task<Payment?> GetByOrderIdAsync(int orderId);
    }
}
