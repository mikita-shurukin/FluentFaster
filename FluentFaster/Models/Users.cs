using Microsoft.AspNetCore.Identity;

namespace FluentFaster.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}
