using Cafe.Application.DTOs.Payments;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Validators.Payments
{
    public class PaymentCreateDtoValidator : AbstractValidator<PaymentCreateDto>
    {
        public PaymentCreateDtoValidator()
        {
            RuleFor(x => x.OrderId).GreaterThan(0).WithMessage("Geçerli bir Sipariş ID giriniz.");
            RuleFor(x => x.PaymentType).NotEmpty().WithMessage("Ödeme türü boş olamaz.");
        }
    }
}
