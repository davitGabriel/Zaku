using System;
using System.Collections.Generic;
using System.Text;
using Zaku.Application.Auth.DTOs;

namespace Zaku.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest request);
        Task<AuthResponse> LoginAsync(LoginRequest request);
    }
}
