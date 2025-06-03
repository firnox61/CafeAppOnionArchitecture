using Cafe.Domain.Entities;
using Cafe.Core.Utilities.Results;

namespace Cafe.Application.Interfaces.Services.Contracts
{

    public interface IDailySalesSummaryService
    {
        Task<IResult> UpdateDailySalesAsync(decimal amount, DateTime date);
        Task<IDataResult<List<DailySalesSummary>>> GetLast30DaysAsync();
    }

}
