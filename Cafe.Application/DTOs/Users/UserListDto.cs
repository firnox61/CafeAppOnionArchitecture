﻿using Cafe.Core.Abstractions;
namespace Cafe.Application.DTOs.Users
{
    public class UserListDto : IDto
    {
        public int Id { get; set; }
        public string FullName { get; set; } // FirstName + LastName birleştirilmiş
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
