using Cafe.Application.DTOs.Tables;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Validators.Tables
{
    public class TableUpdateDtoValidator : AbstractValidator<TableUpdateDto>
    {
        public TableUpdateDtoValidator()
        {
            RuleFor(x => x.Id).GreaterThan(0).WithMessage("Geçerli bir Id girin");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Masa ismi boş olamaz");
        }
    }
}
