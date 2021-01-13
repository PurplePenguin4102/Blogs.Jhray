using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Blogs.Jhray.Data;
using System.Threading.Tasks;
using Blogs.Jhray.Database.Entities;
using Microsoft.JSInterop;

namespace Blogs.Jhray.Pages.Memes
{
    public class BlogContainerBase : ComponentBase
    {
        protected List<Posts> blogPosts;
        

        [Parameter]
        public bool AutoLoad { get; set; } = true;
        [Parameter]
        public bool ShowManageWorkflow { get; set; } = false;

        [Parameter]
        public EventCallback<long> EditPostCallback { get; set; }

        [Parameter]
        public long ShowId { get; set; }

        [Inject]
        public BlogService BlogService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override async Task OnInitializedAsync()
        {
            if (AutoLoad)
            {
                if (ShowId > 0)
                {
                    GetPostById();
                }
                else
                {
                    ReloadPosts();
                }
            }
            await Task.CompletedTask;
        }

        public void ReloadPosts()
        {
            //only load one
            var postId = BlogService.GetLatestPostId();
            blogPosts = new List<Posts> { BlogService.GetPostReadOnly(postId) };
            // lazy load the rest
            Task.Run(async () =>
            {
                blogPosts.AddRange(BlogService.ListPosts().OrderByDescending(p => p.PublishDate).ToList());
                await InvokeAsync(() => StateHasChanged());
            });

            
            StateHasChanged();
        }

        public void GetPostById(long? showId = null)
        {
            ShowId = showId ?? ShowId;
            blogPosts = new List<Posts> { BlogService.GetPostReadOnly(ShowId) };
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
