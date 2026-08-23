using MediatR;

namespace GreenBill.Identity.Application.Authentication.Login
{
    public class LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
