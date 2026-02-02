using System;
using System.Collections.Generic;
using System.Text;

namespace Zaku.Application.DTOs.Auth
{
    public record LoginRequest
    {
        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}
