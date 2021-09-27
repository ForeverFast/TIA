using Microsoft.AspNetCore.Identity;

namespace TIA.Core.AspNetCoreEntities
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
