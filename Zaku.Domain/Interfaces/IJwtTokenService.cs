using Zaku.Domain.Entities;

namespace Zaku.Domain.Interfaces
{
    public interface IJwtTokenService
    {
        string CreateToken(User user);
    }
}
