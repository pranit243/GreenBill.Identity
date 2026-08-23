using GreenBill.Identity.Application.Authentication.Interfaces;
using MediatR;

namespace GreenBill.Identity.Application.Authentication.Revoke
{
    public class RevokeTokenCommandHandler : IRequestHandler<RevokeTokenCommand>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly ITokenService _tokenService;

        public RevokeTokenCommandHandler(
            IRefreshTokenRepository refreshTokenRepository,
            ITokenService tokenService)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _tokenService = tokenService;
        }

        public async Task Handle(
            RevokeTokenCommand request,
            CancellationToken cancellationToken)
        {
            var tokenHash = _tokenService.HashToken(request.RefreshToken);

            var stored = await _refreshTokenRepository
                .GetActiveByTokenHashAsync(tokenHash, cancellationToken);

            if (stored is null)
                return; // Already expired/revoked — treat as no-op

            stored.RevokedAtUtc = DateTime.UtcNow;

            await _refreshTokenRepository.SaveChangesAsync(cancellationToken);
        }
    }
}
