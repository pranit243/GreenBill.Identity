using GreenBill.Identity.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenBill.Identity.Infrastructure.Persistent
{
    public class IdentityDbContext:DbContext
    {
        public IdentityDbContext(DbContextOptions<IdentityDbContext> option):base(option)
        {

        }

        public DbSet<User> Users => Set<User>();

        public DbSet<Role> Roles => Set<Role>();

        public DbSet<UserRole> UserRoles => Set<UserRole>();

        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(
                typeof(IdentityDbContext).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
