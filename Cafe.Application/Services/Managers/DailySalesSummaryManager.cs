using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Application.Repositories;
using Cafe.Application.Utilities.Results;
using Cafe.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Services.Managers
{
    public class DailySalesSummaryManager : IDailySalesSummaryService
    {
        private readonly IDailySalesSummaryDal _dailySalesSummaryDal;

        public DailySalesSummaryManager(IDailySalesSummaryDal dailySalesSummaryDal)
        {
            _dailySalesSummaryDal = dailySalesSummaryDal;
        }

        public async Task<IResult> UpdateDailySalesAsync(decimal amount, DateTime date)
        {
            var today = date.Date;
            var summary = await _dailySalesSummaryDal.GetAsync(s => s.Date == today);

            if (summary != null)
            {
                summary.TotalAmount += amount;
                await _dailySalesSummaryDal.UpdateAsync(summary);
            }
            else
            {
                await _dailySalesSummaryDal.AddAsync(new DailySalesSummary
                {
                    Date = today,
                    TotalAmount = amount
                });
            }

            return new SuccessResult("Günlük ciro güncellendi.");
        }

        public async Task<IDataResult<List<DailySalesSummary>>> GetLast30DaysAsync()
        {
            var limit = DateTime.Today.AddDays(-30);
            var result = await _dailySalesSummaryDal.GetAllAsync(s => s.Date >= limit);
            return new SuccessDataResult<List<DailySalesSummary>>(result);
        }
    }

}
