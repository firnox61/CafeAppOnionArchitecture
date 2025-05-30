using Cafe.Application.Utilities.Results;
using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Interfaces.Services.Contracts
{

    public interface IDailySalesSummaryService
    {
        Task<IResult> UpdateDailySalesAsync(decimal amount, DateTime date);
        Task<IDataResult<List<DailySalesSummary>>> GetLast30DaysAsync();
    }

}
