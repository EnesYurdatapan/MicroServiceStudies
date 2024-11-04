using Microsoft.AspNetCore.Identity;

namespace Ms.Services.AuthAPI.Models
{
    public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }
    }
}
