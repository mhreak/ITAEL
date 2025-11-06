using Microsoft.AspNetCore.Identity;

namespace DbEntities.Identity
{
    public class CustomRole : IdentityRole<int>
    {
        public CustomRole() { }
        public CustomRole(string name) { Name = name; }
    }
}
