using GreenBill.Identity.Domain.Entity;

namespace GreenBill.Identity.Application.Authentication.Interfaces
{
    public interface ITokenService
    {
        string GenerateAccessToken(User user, string role);
        string GenerateRefreshToken();
        string HashToken(string token);
    }
}
