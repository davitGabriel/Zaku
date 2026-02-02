using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Zaku.Application.DTOs.Auth
{
    public record AuthResponse
    {
        public string? AccessToken;
        public string? UserId;
        public string? Email;
    }
}
