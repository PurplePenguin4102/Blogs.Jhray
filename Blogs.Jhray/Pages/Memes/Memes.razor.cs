using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Blogs.Jhray.Data;
using System.Threading.Tasks;
using Markdig;
using Blogs.Jhray.Database.Entities;

namespace Blogs.Jhray.Pages
{
    public class MemesBase : ComponentBase
    {
        protected List<Posts> blogPosts;

        [Parameter]
        public EventCallback<int> SignalReload { get; set; }

        [Inject]
        public BlogService BlogService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            ReloadPosts();
            await Task.CompletedTask;
        }

        public void ReloadPosts()
        {
            blogPosts = BlogService.ListPosts().OrderByDescending(p => p.PublishDate).ToList();
            StateHasChanged();
        }

        public void ReloadPostById(long id)
        {
            blogPosts = BlogService.ListPosts();
        }

        public async Task DeletePostById(long id)
        {
            await BlogService.DeletePostById(id);
            ReloadPosts();
        }
    }
}
