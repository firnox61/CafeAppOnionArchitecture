using Cafe.Core.Abstractions;
namespace Cafe.Application.DTOs.Users
{
    public class UsageReportFilterDto : IDto
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
