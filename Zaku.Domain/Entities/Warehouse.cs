using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaku.Domain.Entities
{
    public class Warehouse : Entity
    {
        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        public string? Name { get; set; }

        public string? Address { get; set; }

        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int Capacity { get; set; }

        // Navigation property
        public Supplier? Supplier { get; set; }
    }
}
