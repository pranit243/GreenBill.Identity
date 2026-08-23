using FluentValidation;
using GreenBill.Identity.Domain.Constants;

namespace GreenBill.Identity.Application.Authentication.Register
{
    public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
    {
        private static readonly string[] ValidRoles =
            [RoleNames.Owner, RoleNames.Merchant, RoleNames.Customer, RoleNames.Partner];

        public RegisterCommandValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty()
                .EmailAddress()
                .MaximumLength(256);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(8)
                .MaximumLength(100);

            RuleFor(x => x.PhoneNumber)
                .MaximumLength(20)
                .When(x => x.PhoneNumber is not null);

            RuleFor(x => x.RoleName)
                .NotEmpty()
                .Must(r => ValidRoles.Contains(r))
                .WithMessage(
                    $"RoleName must be one of: {string.Join(", ", ValidRoles)}");
        }
    }
}
