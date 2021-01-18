using Blogs.Jhray.Persistence.Database.Entities;
using Blogs.Jhray.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Pages.ManageBlogs
{
    public partial class AddBlog : ComponentBase 
    {
        [Inject]
        public BlogService BlogService { get; set; }

        public List<Blog> Blogs { get; set; }
        public EditContext NewBlogContext { get; set; }
        private AddBlogModel _newBlogModel { get; set; } = new AddBlogModel();

        protected override void OnInitialized()
        {
            Blogs = BlogService.ListBlogs();
            NewBlogContext = new EditContext(_newBlogModel);
        }
        
        protected async Task OnNewBlog(EditContext data)
        {
            if (_newBlogModel.Id == 0)
            {
                await BlogService.AddBlog(_newBlogModel);
            }
            else
            {
                await BlogService.UpdateBlog(_newBlogModel);
            }
            Blogs = BlogService.ListBlogs();
            StateHasChanged();
        }

        protected void Clear()
        {
            _newBlogModel = new AddBlogModel();
        }

        protected void SetEditContext(long id)
        {
            var blog = Blogs.First(b => b.Id == id);
            _newBlogModel = new AddBlogModel()
            {
                Id = blog.Id,
                Author = blog.Author,
                LeadIn = blog.LeadIn,
                Title = blog.Title,
                HomeUrl = blog.HomeUrl
            };
        }

        protected async Task Delete(long id)
        {
            await BlogService.DeleteBlogById(id);
        }
    }
}
