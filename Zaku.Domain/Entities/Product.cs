using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Zaku.Domain.Entities
{
    public class Product : Entity
    {
        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }
        
        public string? SKU { get; set; }
        
        public string? Name { get; set; }
        
        public string? Description { get; set; }
        
        public decimal UnitPrice { get; set; }
        
        public string? Unit { get; set; }
        
        public bool IsActive { get; set; }

        // Navigation property
        public Supplier? Supplier { get; set; }
    }
}
