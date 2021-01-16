using Blogs.Jhray.Persistence.Database.Entities;
using Blogs.Jhray.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Pages.BlogContent
{
    public partial class Home : ComponentBase
    {
        [Inject]
        public BlogService BlogService { get; set; }
        [Parameter] 
        public string HomeUrl { get; set; }

        private bool _isFound { get; set; }

        public Blog Blog { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                Blog = BlogService.GetBlogMetadata(HomeUrl);
                _isFound = true;
            }
            catch (KeyNotFoundException)
            {
                _isFound = false;
            }
        }
    }
}
