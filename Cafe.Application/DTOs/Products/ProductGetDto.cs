﻿using Cafe.Application.DTOs.ProductIngredients;
using Cafe.Core.Abstractions;

namespace Cafe.Application.DTOs.Products
{
    public class ProductGetDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Description { get; set; } = null!;
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public string? ImageFileName { get; set; }

        //public List<ProductIngredientGetDto> ProductIngredients { get; set; } = new();
        public List<ProductIngredientDto> Ingredients { get; set; } = new();
    }
}
