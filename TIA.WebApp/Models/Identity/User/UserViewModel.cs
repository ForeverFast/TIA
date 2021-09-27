using System.Collections.Generic;

namespace TIA.WebApp.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }

        public string Email { get; set; }

        public int Year { get; set; }

        public IList<string> Roles { get; set; }
    }
}
