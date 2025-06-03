
using Cafe.Application.DTOs.Users;
using Cafe.Application.Interfaces.Services.Contracts;
using Cafe.Infrastructure.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace Cafe.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IProductIngredientService _productIngredientService;
        private readonly IDailySalesSummaryService _dailySalesSummaryService;

        public ReportsController(IProductIngredientService productIngredientService, IDailySalesSummaryService dailySalesSummaryService)
        {
            _productIngredientService = productIngredientService;
            _dailySalesSummaryService = dailySalesSummaryService;
        }

        [HttpPost("exportmostusedingredients-excel")]
        public async Task<IActionResult> ExportMostUsedIngredientsExcel([FromBody] UsageReportFilterDto filter)
        {
            var result = await _productIngredientService.GetMostUsedIngredientsAsync(filter);

            if (!result.Success || result.Data == null || !result.Data.Any())
                return BadRequest("Veri bulunamadı veya boş.");

            var excelBytes = ExcelExportHelper.ExportIngredientUsageToExcel(result.Data);

            return File(
                excelBytes,
                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                $"EnCokKullanilanMalzemeler_{DateTime.Now:yyyyMMddHHmm}.xlsx"
            );
        }
        /// <summary>
        /// Son 30 güne ait günlük satış özetlerini döner.
        /// </summary>
        [HttpGet("daily-sales")]
        public async Task<IActionResult> GetDailySales()
        {
            var result = await _dailySalesSummaryService.GetLast30DaysAsync();

            if (result.Success)
                return Ok(result);

            return BadRequest(result);
        }
    }
}
