using Cafe.Application.DTOs.Tables;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Validators.Tables
{
    public class TableCreateDtoValidator : AbstractValidator<TableCreateDto>
    {
        public TableCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Masa ismi boş olamaz");
        }
    }
}
