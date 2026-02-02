using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaku.Domain.Entities
{
    public class Delivery : Entity
    {
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }

        // References a User with UserType = Courier; nullable
        [ForeignKey(nameof(Courier))]
        public Guid? CourierId { get; set; }

        [ForeignKey(nameof(Schedule))]
        public Guid? ScheduleId { get; set; }

        public DeliveryStatus Status { get; set; }

        public DateTime ScheduledDateTime { get; set; }

        public DateTime? ActualDateTime { get; set; }

        public string? DeliveryAddress { get; set; }

        public string? DeliveryInstructions { get; set; }

        // URL to blob storage or similar
        public string? ProofOfDelivery { get; set; }

        // Navigation properties
        public Order? Order { get; set; }
        public User? Courier { get; set; }
        public DeliverySchedule? Schedule { get; set; }
    }

    public enum DeliveryStatus
    {
        Scheduled = 1,
        InTransit = 2,
        Delivered = 3,
        Failed = 4
    }
}
