using Blogs.Jhray.Persistence.Database.Entities;
using Blogs.Jhray.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Shared
{
    public partial class NavMenu : ComponentBase
    {
        [Inject]
        public BlogService BlogService { get; set; }

        public List<Blog> Blogs { get; set; }

        protected override void OnInitialized()
        {
            Blogs = BlogService.ListBlogs();
        }


    }
}
