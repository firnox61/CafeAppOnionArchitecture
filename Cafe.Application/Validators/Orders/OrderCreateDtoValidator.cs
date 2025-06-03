using Cafe.Application.DTOs.Orders;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Validators.Orders
{
    public class OrderCreateDtoValidator : AbstractValidator<OrderCreateDto>
    {
        public OrderCreateDtoValidator()
        {
            RuleFor(x => x.TableId)
                .GreaterThan(0).WithMessage("Geçerli bir masa seçilmelidir.");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("Siparişe en az bir ürün eklenmelidir.");

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
