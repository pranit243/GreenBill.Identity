namespace GreenBill.Identity.Domain.Entity
{
    public class RefreshToken
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public string TokenHash { get; set; } = null!;

        public DateTime ExpiresAtUtc { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public DateTime? RevokedAtUtc { get; set; }

        public User User { get; set; } = null!;

        public bool IsExpired =>
            DateTime.UtcNow >= ExpiresAtUtc;

        public bool IsRevoked =>
            RevokedAtUtc.HasValue;

        public bool IsActive =>
            !IsExpired && !IsRevoked;
    }
}
