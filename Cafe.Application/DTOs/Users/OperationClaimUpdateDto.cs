﻿using Cafe.Core.Abstractions;
namespace Cafe.Application.DTOs.Users
{
    public class OperationClaimUpdateDto : IDto
    {
        public int Id { get; set; } // 🟢 güncellenecek ID burada gelmeli
        public string Name { get; set; }
    }
}
