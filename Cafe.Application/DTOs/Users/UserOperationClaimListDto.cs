using Cafe.Core.Abstractions;
namespace Cafe.Application.DTOs.Users
{
    public class UserOperationClaimListDto : IDto
    {
        public int Id { get; set; }
        public string UserFullName { get; set; }
        public string RoleName { get; set; }
    }
}
