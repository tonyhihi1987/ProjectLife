using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace ProjectLife.Model.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}