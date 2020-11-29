using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

namespace Blogs.Jhray.Security
{
    public class EmailHandler : IAuthorizationHandler
    {
        public Task HandleAsync(AuthorizationHandlerContext context)
        {
            var pendingRequirements = context.PendingRequirements;
            foreach (var req in pendingRequirements)
            {
                if (req is EmailRequirement)
                {
                    if (IsMe(context, req as EmailRequirement))
                    {
                        context.Succeed(req);
                    }
                }
            }
            return Task.CompletedTask;
        }

        private static bool IsMe(AuthorizationHandlerContext context, EmailRequirement req)
        {
            return req.Email == context.User.Identity.Name;
        }
    }
}