using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Zaku.Domain.Enums;

namespace Zaku.Domain.Entities
{
    public class ProductRequest : Entity
    {
        [ForeignKey(nameof(Client))]
        public Guid ClientId { get; set; }

        public string? ProductName { get; set; }

        public string? Description { get; set; }

        public int Quantity { get; set; }

        public DateTime RequiredByDate { get; set; }

        public decimal MaxPrice { get; set; }

        public ProductRequestStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime ExpiresAt { get; set; }

        // Navigation
        public Client? Client { get; set; }
    }
}
