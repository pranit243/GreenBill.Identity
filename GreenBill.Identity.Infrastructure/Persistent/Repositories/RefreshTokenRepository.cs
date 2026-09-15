using GreenBill.Identity.Application.Authentication.Interfaces;
using GreenBill.Identity.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace GreenBill.Identity.Infrastructure.Persistent.Repositories
{
    public class RefreshTokenRepository : IRefreshTokenRepository
    {
        private readonly IdentityDbContext _context;

        public RefreshTokenRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<RefreshToken?> GetActiveByTokenHashAsync(
            string tokenHash,
            CancellationToken cancellationToken = default)
        {
            return await _context.RefreshTokens
                .FirstOrDefaultAsync(
                    x => x.TokenHash == tokenHash
                         && !x.RevokedAtUtc.HasValue
                         && x.ExpiresAtUtc > DateTime.UtcNow,
                    cancellationToken);
        }

        public async Task AddAsync(
            RefreshToken token,
            CancellationToken cancellationToken = default)
        {
            await _context.RefreshTokens.AddAsync(token, cancellationToken);
        }

        public async Task SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
