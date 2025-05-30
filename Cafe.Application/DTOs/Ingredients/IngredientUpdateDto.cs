﻿using Cafe.Application.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Application.DTOs.Ingredients
{
    public class IngredientUpdateDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Unit { get; set; } = null!; // örn: gram, ml, adet
        public double Stock { get; set; }
    }
}
