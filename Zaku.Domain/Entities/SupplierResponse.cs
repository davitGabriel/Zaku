using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaku.Domain.Entities
{
    public class SupplierResponse : Entity
    {
        [ForeignKey(nameof(Request))]
        public Guid RequestId { get; set; }

        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }

        public decimal OfferedPrice { get; set; }

        public int Quantity { get; set; }

        public DateTime DeliveryDate { get; set; }

        public string? Notes { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public ProductRequest? Request { get; set; }
        public Supplier? Supplier { get; set; }
        public Product? Product { get; set; }
    }
}
