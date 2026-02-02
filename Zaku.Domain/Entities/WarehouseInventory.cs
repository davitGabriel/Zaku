using System.ComponentModel.DataAnnotations.Schema;

namespace Zaku.Domain.Entities
{
    public class WarehouseInventory : Entity
    {
        [ForeignKey(nameof(Warehouse))]
        public Guid WarehouseId { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public int Quantity { get; set; }

        public int ReorderLevel { get; set; }

        public DateTime LastUpdated { get; set; }

        // Navigation properties
        public Warehouse? Warehouse { get; set; }
        public Product? Product { get; set; }
    }
}
