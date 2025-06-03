using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Tables
{
    public class TableUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // Örn: Masa 1
    }
}
