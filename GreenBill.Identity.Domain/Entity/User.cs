namespace GreenBill.Identity.Domain.Entity
{
    public class User
    {
        public Guid Id { get; set; }

        public string Email { get; set; } = null!;

        public string? PhoneNumber { get; set; }

        public string PasswordHash { get; set; } = null!;

        public bool IsActive { get; set; }

        public DateTime CreatedAtUtc { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
            = new List<UserRole>();

        public ICollection<RefreshToken> RefreshTokens { get; set; }
            = new List<RefreshToken>();
    }
}
