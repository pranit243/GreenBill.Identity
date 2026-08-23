using MediatR;

namespace GreenBill.Identity.Application.Authentication.Refresh
{
    public class RefreshTokenCommand : IRequest<RefreshTokenResponse>
    {
        public string RefreshToken { get; set; } = null!;
    }
}
