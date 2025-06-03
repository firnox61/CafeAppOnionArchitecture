﻿using Cafe.Application.DTOs.Users;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Validators.Users
{
    public class UserOperationClaimCreateDtoValidator : AbstractValidator<UserOperationClaimCreateDto>
    {
        public UserOperationClaimCreateDtoValidator()
        {
            RuleFor(x => x.UserId)
                .GreaterThan(0).WithMessage("Geçerli bir kullanıcı seçilmelidir.");

            RuleFor(x => x.OperationClaimId)
                .GreaterThan(0).WithMessage("Geçerli bir rol seçilmelidir.");
        }
    }
}
