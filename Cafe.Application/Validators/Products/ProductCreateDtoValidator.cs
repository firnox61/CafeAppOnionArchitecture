using Cafe.Application.DTOs.Products;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.Validators.Products
{
    public class ProductCreateDtoValidator : AbstractValidator<ProductCreateDto>
    {
        public ProductCreateDtoValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Ürün adı boş olamaz.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Fiyat 0'dan büyük olmalı.");
            RuleFor(x => x.Stock).GreaterThanOrEqualTo(0).WithMessage("Stok negatif olamaz.");
            RuleFor(x => x.ProductIngredients).NotEmpty().WithMessage("En az 1 malzeme girilmelidir.");
        }
    }
}
