using MediatR;

namespace GreenBill.Identity.Application.Authentication.Revoke
{
    public class RevokeTokenCommand : IRequest
    {
        public string RefreshToken { get; set; } = null!;
    }
}
