using Microsoft.AspNetCore.Identity;

namespace AstractTask.Infrastruture.Identity
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }
}