﻿using Cafe.Application.DTOs.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Validators.Users
{
    public class UserUpdateDtoValidator : AbstractValidator<UserUpdateDto>
    {
        public UserUpdateDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli kullanıcı ID'si girilmelidir");
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Ad boş olamaz");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Soyad boş olamaz");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Geçerli bir e-posta giriniz");
        }
    }
}
