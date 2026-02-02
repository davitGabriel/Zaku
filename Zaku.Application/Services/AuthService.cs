using Zaku.Application.Auth.DTOs;
using Zaku.Application.Interfaces;
using Zaku.Domain.Entities;
using Zaku.Domain.Enums;
using Zaku.Domain.Interfaces;

namespace Zaku.Application.Services
{
    internal class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;

        public AuthService(
            IRepository<User> userRepository,
            IUnitOfWork unitOfWork,
            IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
        {
            if (await _userRepository.AnyAsync(u => u.Email == request.Email))
                throw new InvalidOperationException("Email already registered");

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            var user = new User
            {
                Id = Guid.NewGuid(),
                Email = request.Email!,
                PasswordHash = passwordHash,
                UserType = (UserType)Enum.Parse(typeof(UserType), request.UserType!, true),
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            await _userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            var token = _jwtTokenService.CreateToken(user);

            return new AuthResponse { AccessToken = token, UserId = user.Id.ToString() };
        }

        public async Task<AuthResponse> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash))
                throw new UnauthorizedAccessException("Invalid credentials");

            var token = _jwtTokenService.CreateToken(user);

            return new AuthResponse
            {
                AccessToken = token,
                UserId = user.Id.ToString(),
                Email = user.Email
            };
        }
    }
}
