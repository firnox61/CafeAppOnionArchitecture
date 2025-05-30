using Cafe.Domain.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cafe.Domain.Entities
{
    public class ProductionHistory : IEntity
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }  // ❗ Artık Nullable
        public int QuantityProduced { get; set; }
        public DateTime ProducedAt { get; set; }

        public Product? Product { get; set; }  // ❗ Nullable olmalı çünkü silinince null olacak
    }
}
