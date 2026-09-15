using GreenBill.Identity.Application.Authentication.Interfaces;
using GreenBill.Identity.Domain.Entity;
using MediatR;

namespace GreenBill.Identity.Application.Authentication.Register
{
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisterResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRoleRepository _roleRepository;

        public RegisterCommandHandler(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IRoleRepository roleRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _roleRepository = roleRepository;
        }

        public async Task<RegisterResponse> Handle(
            RegisterCommand request,
            CancellationToken cancellationToken)
        {
            var email = request.Email.Trim().ToLowerInvariant();

            var exists = await _userRepository
                .ExistsByEmailAsync(email, cancellationToken);

            if (exists)
                throw new InvalidOperationException(
                    "A user with this email already exists.");

            var role = await _roleRepository
                .GetByNameAsync(request.RoleName, cancellationToken)
                    ?? throw new InvalidOperationException(
                        $"Role '{request.RoleName}' not found. Ensure roles are seeded.");

            var userId = Guid.NewGuid();

            var user = new User
            {
                Id = userId,
                Email = email,
                PhoneNumber = request.PhoneNumber,
                PasswordHash = _passwordHasher.Hash(request.Password),
                IsActive = true,
                CreatedAtUtc = DateTime.UtcNow,
                UserRoles = new List<UserRole>
                {
                    new UserRole { UserId = userId, RoleId = role.Id }
                }
            };

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);

            return new RegisterResponse
            {
                UserId = user.Id,
                Email = user.Email,
                Role = role.Name
            };
        }
    }
}
