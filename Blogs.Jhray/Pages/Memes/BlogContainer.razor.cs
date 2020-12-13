using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Blogs.Jhray.Data;
using System.Threading.Tasks;
using Markdig;
using Blogs.Jhray.Database.Entities;
using Microsoft.JSInterop;

namespace Blogs.Jhray.Pages.Memes
{
    public class BlogContainerBase : ComponentBase
    {
        protected List<Posts> blogPosts;

        [Parameter]
        public bool ShowManageWorkflow { get; set; } = false;

        [Parameter]
        public EventCallback<long> EditPostCallback { get; set; }

        [Parameter]
        public long ShowId { get; set; } = -1;

        [Inject]
        public BlogService BlogService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (ShowId > -1)
            {

            }
            else
            {
                ReloadPosts();
            }
            
            await Task.CompletedTask;
        }

        public void ReloadPosts()
        {
            blogPosts = BlogService.ListPosts().OrderByDescending(p => p.PublishDate).ToList();
            StateHasChanged();
        }

        public void GetPostById()
        {
            blogPosts = BlogService.GetPost(ShowId);
            StateHasChanged();
        }

        public void ReloadPostById(long id)
        {
            blogPosts = BlogService.ListPosts();
        }

        public async Task DeletePostById(long id)
        {
            if (await JSRuntime.InvokeAsync<bool>("confirm", new[] { "Are you sure you want to delete this post?" }))
            {
                await BlogService.DeletePostById(id);
            }
            ReloadPosts();
        }

        public async Task EditPostById(long id)
        {
            await EditPostCallback.InvokeAsync(id);
            await JSRuntime.InvokeAsync<object>("scrollTo", new object[] { 0, 0 });
        }


    }
}
