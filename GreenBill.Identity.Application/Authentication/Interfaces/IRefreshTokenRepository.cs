using GreenBill.Identity.Domain.Entity;

namespace GreenBill.Identity.Application.Authentication.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task<RefreshToken?> GetActiveByTokenHashAsync(
            string tokenHash,
            CancellationToken cancellationToken = default);

        Task AddAsync(
            RefreshToken token,
            CancellationToken cancellationToken = default);

        Task SaveChangesAsync(
            CancellationToken cancellationToken = default);
    }
}
