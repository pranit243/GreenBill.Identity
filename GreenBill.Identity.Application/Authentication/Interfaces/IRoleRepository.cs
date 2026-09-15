using GreenBill.Identity.Domain.Entity;

namespace GreenBill.Identity.Application.Authentication.Interfaces
{
    public interface IRoleRepository
    {
        Task<Role?> GetByNameAsync(
            string name,
            CancellationToken cancellationToken = default);
    }
}
