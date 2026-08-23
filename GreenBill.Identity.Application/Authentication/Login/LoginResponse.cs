namespace GreenBill.Identity.Application.Authentication.Login
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = null!;

        public string RefreshToken { get; set; } = null!;

        public Guid UserId { get; set; }

        public string Email { get; set; } = null!;

        public IEnumerable<string> Roles { get; set; } = [];
    }
}
