using Cafe.Application.DTOs.Orders;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Validators.Orders
{
    public class OrderUpdateDtoValidator : AbstractValidator<OrderUpdateDto>
    {
        public OrderUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir sipariş ID'si gereklidir.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Güncellenen siparişte en az bir ürün olmalıdır.");

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId)
                    .GreaterThan(0).WithMessage("Geçerli bir ürün seçilmelidir.");

                items.RuleFor(i => i.Quantity)
                    .GreaterThan(0).WithMessage("Ürün adedi en az 1 olmalıdır.");
            });
        }
    }
}
