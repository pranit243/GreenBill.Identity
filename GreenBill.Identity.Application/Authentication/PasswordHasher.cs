using GreenBill.Identity.Application.Authentication.Interfaces;
using GreenBill.Identity.Entity;
using Microsoft.AspNetCore.Identity;
using IdentityPasswordHasher = Microsoft.AspNetCore.Identity.PasswordHasher<GreenBill.Identity.Entity.User>;

namespace GreenBill.Identity.Application.Authentication
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<User> _passwordHasher;

        public PasswordHasher()
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        public string Hash(string password)
        {
            return _passwordHasher.HashPassword(
                null!,
                password);
        }

        public bool Verify(
            string password,
            string passwordHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(
                null!,
                passwordHash,
                password);

            return result == PasswordVerificationResult.Success ||
                   result == PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
