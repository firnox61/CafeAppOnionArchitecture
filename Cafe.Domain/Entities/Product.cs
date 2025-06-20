﻿using Cafe.Core.Abstractions;

namespace Cafe.Domain.Entities
{
    public class Product : IEntity
    {
        public int Id { get; set; }
      
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int MinStockThreshold { get; set; } = 0; // ✅ Kritik stok eşiği
        public DateTime CreatedAt { get; set; }
        public string? ImageFileName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();
        public ICollection<ProductionHistory> ProductionHistories { get; set; } = new List<ProductionHistory>();

    }
}
