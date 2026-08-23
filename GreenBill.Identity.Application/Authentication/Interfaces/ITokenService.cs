using GreenBill.Identity.Entity;

namespace GreenBill.Identity.Application.Authentication.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, IEnumerable<string> roles);
        string GenerateRefreshToken();
        string HashToken(string token);
    }
}
