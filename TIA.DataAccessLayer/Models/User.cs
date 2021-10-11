using Microsoft.AspNetCore.Identity;
using System;

namespace TIA.DataAccessLayer.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }

        public string RefreshToken { get; set; }
        public DateTime RefreshTokenExpiryTime { get; set; }
    }
}
