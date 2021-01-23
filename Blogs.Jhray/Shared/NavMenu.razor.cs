using Blogs.Jhray.Pages.ManageBlogs;
using Blogs.Jhray.Persistence.Database.Entities;
using Blogs.Jhray.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Shared
{
    public partial class NavMenu : ComponentBase, IDisposable
    {
        [Inject]
        public BlogService BlogService { get; set; }

        public List<Blog> Blogs { get; set; }

        public void Dispose()
        {
            AddBlog.SystemChangeEvent -= ReloadBlogs;
        }

        protected override void OnInitialized()
        {
            Blogs = BlogService.ListBlogs();
            AddBlog.SystemChangeEvent += ReloadBlogs;
        }

        private void ReloadBlogs(object sender, EventArgs e)
        {
            Blogs = BlogService.ListBlogs();
            StateHasChanged();
        }

        
    }
}
