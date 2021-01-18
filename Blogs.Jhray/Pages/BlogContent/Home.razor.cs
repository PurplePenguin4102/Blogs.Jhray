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

        private string _homeUrl;
        [Parameter] 
        public string HomeUrl 
        { 
            get => _homeUrl;
            set
            {
                if (_homeUrl != value)
                {
                    _homeUrl = value;
                    LoadBlog(value);
                    StateHasChanged();
                }
            } 
        }


        private bool _isFound { get; set; }

        public Blog Blog { get; set; }

        protected override void OnInitialized()
        {
            try
            {
                LoadBlog(HomeUrl);
                _isFound = true;
            }
            catch (KeyNotFoundException)
            {
                _isFound = false;
            }
        }

        protected void LoadBlog(string homeUrl)
        {
            Blog = BlogService.GetBlogMetadata(homeUrl);
        }
    }
}
