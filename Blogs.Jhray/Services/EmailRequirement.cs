using Microsoft.AspNetCore.Authorization;

namespace Blogs.Jhray.Services
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