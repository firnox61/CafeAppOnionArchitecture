using Cafe.Application.DTOs.Ingredients;
using FluentValidation;

namespace Cafe.Application.Validators.Ingredients
{
    public class IngredientUpdateDtoValidator : AbstractValidator<IngredientUpdateDto>
    {
        public IngredientUpdateDtoValidator()
        {
            RuleFor(x => x.Id)
                .GreaterThan(0).WithMessage("Geçerli bir ID girilmelidir.");

            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Malzeme adı zorunludur.")
                .MaximumLength(100);

            RuleFor(x => x.Unit)
                .NotEmpty().WithMessage("Birim bilgisi zorunludur.")
                .MaximumLength(20);

            RuleFor(x => x.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("Stok miktarı negatif olamaz.");
        }
    }
}