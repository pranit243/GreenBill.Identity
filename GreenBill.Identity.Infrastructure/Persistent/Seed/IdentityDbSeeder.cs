using GreenBill.Identity.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenBill.Identity.Infrastructure.Persistent.Seed
{
    public class IdentityDbSeeder
    {
        private readonly IdentityDbContext _context;

        public IdentityDbSeeder(IdentityDbContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            List<Role> rolesToSeed = new List<Role>
            {
                new Role{Id = Guid.NewGuid(), Name = "Owner", Description = "GreenBill Owner"},
                new Role{Id = Guid.NewGuid(), Name = "Customer", Description = "GreenBill Customer"},
                new Role{Id = Guid.NewGuid(), Name = "Merchant", Description = "GreenBill Merchant"},
                new Role{Id = Guid.NewGuid(), Name = "Partner", Description = "GreenBill Partner"}
            };

            var existingRoleNames = await _context.Roles
            .Select(x => x.Name)
            .ToListAsync();

            var newRoles = rolesToSeed
                .Where(x => !existingRoleNames.Contains(x.Name))
                .ToList();

            if (newRoles.Any())
            {
                await _context.Roles.AddRangeAsync(newRoles);
                await _context.SaveChangesAsync();
            }
        }
    }
}
