using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaku.Domain.Entities
{
    public class Courier : Entity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        public string? VehicleType { get; set; }

        public string? LicensePlate { get; set; }

        public string? PhoneNumber { get; set; }

        public bool IsAvailable { get; set; }

        // Geography stored as text (WKT/GeoJSON) or map to geography type in DbContext
        public string? CurrentLocation { get; set; }

        // Navigation properties
        public User? User { get; set; }
        public Supplier? Supplier { get; set; }
    }
}
