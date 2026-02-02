using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaku.Domain.Entities
{
    public class DeliverySchedule : Entity
    {
        [ForeignKey(nameof(Supplier))]
        public Guid SupplierId { get; set; }

        [ForeignKey(nameof(Client))]
        public Guid? ClientId { get; set; }

        // JSON stored as text (e.g. { "type": "weekly", "days": ["Mon","Wed"] })
        public string? RecurrencePattern { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsActive { get; set; }

        // Navigation properties
        public Supplier? Supplier { get; set; }
        public Client? Client { get; set; }
    }
}
