using Microsoft.AspNetCore.Identity;

namespace TIA.Core.EfEntities
{
    public class User : IdentityUser
    {
        public int Year { get; set; }

        public string Password { get; set; }
    }
}
