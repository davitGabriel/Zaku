using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaku.Domain.Entities
{
    public class Client : Entity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public string? CompanyName { get; set; }

        public string? TaxId { get; set; }

        public string? DefaultDeliveryAddress { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}
