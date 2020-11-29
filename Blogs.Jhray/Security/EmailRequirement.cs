using Microsoft.AspNetCore.Authorization;

namespace Blogs.Jhray.Security
{
    public class EmailRequirement : IAuthorizationRequirement
    {
        public string Email { get; }

        public EmailRequirement(string email)
        {
            Email = email;
        }
    }
}