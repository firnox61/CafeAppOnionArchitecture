﻿using Cafe.Application.DTOs.Ingredients;
using FluentValidation;

namespace Cafe.Application.Validators.Ingredients
{
    public class IngredientCreateDtoValidator : AbstractValidator<IngredientCreateDto>
    {
      

        public IngredientCreateDtoValidator()
        {
            RuleFor(i => i.Name)
                .NotEmpty()
                .WithMessage("Malzeme ismi boş olamaz!");

            RuleFor(i => i.Unit)
                .NotEmpty()
                .WithMessage("Birim bilgisi boş olamaz!");

            RuleFor(i => i.Stock)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Stok değeri negatif olamaz!");
        }
    }

}
