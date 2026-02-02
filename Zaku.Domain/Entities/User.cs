using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Zaku.Domain.Enums;

namespace Zaku.Domain.Entities
{
    public class User : Entity
    {
        public required string Email { get; set; }
        public required string PasswordHash { get; set; }
        public UserType UserType { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }

        // Navigation properties
        public UserProfile? Profile { get; set; }
    }
}
