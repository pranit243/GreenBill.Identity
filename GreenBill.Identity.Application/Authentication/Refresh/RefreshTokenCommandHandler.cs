using GreenBill.Identity.Application.Authentication.Interfaces;
using GreenBill.Identity.Entity;
using MediatR;

namespace GreenBill.Identity.Application.Authentication.Refresh
{
    public class RefreshTokenCommandHandler : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ITokenService _tokenService;

        public RefreshTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            IUserRepository userRepository,
            ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<RefreshTokenResponse> Handle(
            RefreshTokenCommand request,
            CancellationToken cancellationToken)
        {
            var tokenHash = _tokenService.HashToken(request.RefreshToken);

            var stored = await _refreshTokenRepository
                .GetActiveByTokenHashAsync(tokenHash, cancellationToken);

            if (stored is null)
                throw new UnauthorizedAccessException("Invalid or expired refresh token.");

            var user = await _userRepository
                .GetByIdAsync(stored.UserId, cancellationToken);

            if (user is null || !user.IsActive)
                throw new UnauthorizedAccessException("Account is inactive or not found.");

            // Revoke old token (rotation)
            stored.RevokedAtUtc = DateTime.UtcNow;

            var roles = user.UserRoles
                .Select(ur => ur.Role.Name)
                .ToList();

            var newAccessToken = _tokenService.GenerateAccessToken(user, roles);
            var rawNewRefreshToken = _tokenService.GenerateRefreshToken();

            var newRefreshToken = new RefreshToken
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                TokenHash = _tokenService.HashToken(rawNewRefreshToken),
                CreatedAtUtc = DateTime.UtcNow,
                ExpiresAtUtc = DateTime.UtcNow.AddDays(7)
            };

            await _refreshTokenRepository.AddAsync(newRefreshToken, cancellationToken);
            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);

            return new RefreshTokenResponse
            {
                AccessToken = newAccessToken,
                RefreshToken = rawNewRefreshToken
            };
        }
    }
}
