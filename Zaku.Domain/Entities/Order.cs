using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zaku.Domain.Enums;

namespace Zaku.Domain.Entities
{
    public class Order : Entity
    {
        // Should be unique - configure in DbContext/EF migration
        public string? OrderNumber { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        public OrderType OrderType { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public DateTime RequestedDeliveryDate { get; set; }

        public DateTime? ActualDeliveryDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

        // Navigation properties
        public Client? Client { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
