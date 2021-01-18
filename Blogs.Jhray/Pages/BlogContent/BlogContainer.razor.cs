using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Blogs.Jhray.Services;
using System.Threading.Tasks;
using Blogs.Jhray.Persistence.Database.Entities;
using Microsoft.JSInterop;

namespace Blogs.Jhray.Pages.BlogContent
{
    public partial class BlogContainer : ComponentBase
    {
        protected List<Posts> blogPosts = new List<Posts>();

        private long _blogId;
        [Parameter]
        public long BlogId 
        { 
            get => _blogId; 
            set
            {
                if (_blogId != value)
                {
                    _blogId = value;
                    ReloadPosts();
                }
            }
        }

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
            blogPosts.Clear();
            //only load one
            var postId = BlogService.GetLatestPostId(BlogId);
            if (postId != 0)
            {
                blogPosts = new List<Posts> { BlogService.GetPostReadOnly(postId) };
                // lazy load the rest
                Task.Run(async () =>
                {
                    var blogs = BlogService.ListPosts(BlogId).OrderByDescending(p => p.PublishDate).ToList();
                    blogPosts.AddRange(blogs);
                    await InvokeAsync(() => StateHasChanged());
                });
            }
            StateHasChanged();
        }

        public void GetPostById(long? showId = null)
        {
            ShowId = showId ?? ShowId;
            blogPosts = new List<Posts> { BlogService.GetPostReadOnly(ShowId) };
            StateHasChanged();
        }

        public void ReloadPostById()
        {
            blogPosts = BlogService.ListPosts(BlogId);
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
