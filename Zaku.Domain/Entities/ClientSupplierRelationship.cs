using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaku.Domain.Entities
{
    public class ClientSupplierRelationship : Entity
    {
        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        public bool IsFavorite { get; set; }

        public int OrderCount { get; set; }

        public decimal TotalValue { get; set; }

        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public Client? Client { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
