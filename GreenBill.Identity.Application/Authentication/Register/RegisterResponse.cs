using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreenBill.Identity.Application.Authentication.Register
{
    public class RegisterResponse
    {
        public Guid UserId { get; set; }

        public string Email { get; set; } = null!;

        public string Role { get; set; } = null!;
    }
}
