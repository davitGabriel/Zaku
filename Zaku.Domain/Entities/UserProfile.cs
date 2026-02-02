using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Zaku.Domain.Entities
{
    public class UserProfile : Entity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }

        public string? CompanyName { get; set; }

        public string? Address { get; set; }

        public string? PhoneNumber { get; set; }

        // JSON stored as text
        public string? Preferences { get; set; }

        // Navigation property
        public User? User { get; set; }
    }
}
