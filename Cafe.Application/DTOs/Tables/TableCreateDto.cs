using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Tables
{
    public class TableCreateDto : IDto
    {
        public string Name { get; set; } = null!;
    }
}
