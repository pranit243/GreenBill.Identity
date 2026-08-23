using GreenBill.Identity.Application.Authentication.Interfaces;
using GreenBill.Identity.Entity;
using GreenBill.Identity.Infrastructure.Persistent;
using Microsoft.EntityFrameworkCore;

namespace GreenBill.Identity.Application.Authentication
{
    public class RoleRepository : IRoleRepository
    {
        private readonly IdentityDbContext _context;

        public RoleRepository(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task<Role?> GetByNameAsync(
            string name,
            CancellationToken cancellationToken = default)
        {
            return await _context.Roles
                .FirstOrDefaultAsync(
                    x => x.Name == name,
                    cancellationToken);
        }
    }
}
