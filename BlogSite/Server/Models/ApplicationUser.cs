using Microsoft.AspNetCore.Identity;

namespace BlogSite.Server.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}