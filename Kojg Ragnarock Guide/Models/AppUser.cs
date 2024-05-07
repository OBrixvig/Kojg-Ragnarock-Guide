using Microsoft.AspNetCore.Identity;

namespace Kojg_Ragnarock_Guide.Models
{
    public class AppUser : IdentityUser
    {
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";
        public string Email { get; set; } = "";
        public DateTime CreateAt { get; set; }
    }
}
