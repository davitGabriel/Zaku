using System;
using System.Collections.Generic;
using System.Text;

namespace Zaku.Application.DTOs.Auth
{
    public record RegisterRequest
    {
        public string? Email { get; set; }

        public string? Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? UserType { get; set; }
    }
}
