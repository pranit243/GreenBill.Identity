using GreenBill.Identity.Domain.Constants;
using MediatR;

namespace GreenBill.Identity.Application.Authentication.Register
{
    public class RegisterCommand : IRequest<RegisterResponse>
    {
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        /// <summary>Defaults to Customer; valid values: Owner, Merchant, Customer, Partner.</summary>
        public string RoleName { get; set; } = RoleNames.Customer;
    }
}
