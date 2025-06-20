﻿using Cafe.Core.Abstractions;
namespace Cafe.Application.DTOs.Users
{
    public class UserUpdateDto : IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public bool Status { get; set; }
    }
}
