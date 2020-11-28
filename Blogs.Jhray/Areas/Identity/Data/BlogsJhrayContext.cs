using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Jhray.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Blogs.Jhray.Data
{
    public class BlogsJhrayContext : IdentityDbContext<BlogsJhrayUser>
    {
        public BlogsJhrayContext(DbContextOptions<BlogsJhrayContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
