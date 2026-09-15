using GreenBill.Identity.Domain.Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GreenBill.Identity.Infrastructure.Persistent.Configuration
{
    public class RefreshTokenConfiguration:IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.UserId)
                .IsRequired();

            builder.Property(x => x.TokenHash)
                .IsRequired()
                .HasMaxLength(88);

            builder.HasIndex(x => x.TokenHash)
                .IsUnique();

            builder.HasOne(x => x.User)
                .WithMany(x => x.RefreshTokens)
                .HasForeignKey(x => x.UserId);
        }
    }
}
