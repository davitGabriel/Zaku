using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zaku.Domain.Entities
{
    public class OrderItem : Entity
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        [ForeignKey(nameof(Product))]
        public Guid ProductID { get; set; }

        public int Quantity { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        // Navigation properties
        public Order? Order { get; set; }

        public Product? Product { get; set; }
    }
}
