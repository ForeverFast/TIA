using Microsoft.AspNetCore.Identity;

namespace TIA.DataAccessLayer.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }
}
