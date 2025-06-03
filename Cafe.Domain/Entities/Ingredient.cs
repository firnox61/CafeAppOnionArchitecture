using Cafe.Core.Abstractions;

namespace Cafe.Domain.Entities
{
    public class Ingredient : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Unit { get; set; } = null!; // örn: gram, ml, adet
        public double Stock { get; set; }
        public double MinStockThreshold { get; set; } = 10; // ✅ Kritik eşik seviyesi
        public ICollection<ProductIngredient> ProductIngredients { get; set; } = new List<ProductIngredient>();
    }
}
