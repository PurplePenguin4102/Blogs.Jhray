using Blogs.Jhray.Persistence.Database.Entities;
using Blogs.Jhray.Services;
using Blogs.Jhray.Services.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Pages.BlogContent
{
    public partial class Add : ComponentBase
    {
        [Inject]
        public BlogService BlogService { get; set; }
        [Inject]
        public IJSRuntime JSRuntime { get; set; }
        [Parameter]
        public string HomeUrl { get; set; }
        public Blog Blog { get; set; }
        protected BlogContainer BlogContainer { get; set; }
        protected PostFormData PostFormData { get; set; } = new PostFormData();
        protected bool FormInvalid { get; set; } = true;
        protected EditContext EditContext;
        protected bool IsDirty => EditContext.IsModified();
        protected bool IsEdit { get; set; }

        protected override void OnInitialized()
        {
            EditContext = new EditContext(PostFormData);
            EditContext.OnFieldChanged += HandleFieldChanged;
            Blog = BlogService.GetBlogMetadata(HomeUrl);
        }

        protected async Task ResetForm()
        {
            await ReinitializeForm();
        }

        private void HandleFieldChanged(object sender, FieldChangedEventArgs e)
        {
            FormInvalid = !EditContext.Validate();
            StateHasChanged();
        }

        protected async Task HandleValidSubmit()
        {
            PostFormData.BlogId = Blog.Id;
            await BlogService.AddPost(PostFormData);
            BlogContainer.ReloadPosts();
            await ReinitializeForm();
        }

        protected async Task HandleValidEdit()
        {
            await BlogService.EditPost(PostFormData);
            BlogContainer.ReloadPosts();
            await ReinitializeForm();
        }

        protected async Task EditMode(long id)
        {
            if (IsDirty && !await JSRuntime.InvokeAsync<bool>("confirm", new[] { "Are you sure you want to edit this post?" }))
            {
                return;
            }
            await ReinitializeForm(id);
        }

        private async Task ReinitializeForm(long? id = null)
        {
            EditContext.OnFieldChanged -= HandleFieldChanged;
            PostFormData = new PostFormData();
            if (id.HasValue)
            {
                var post = await BlogService.Find(id.Value);
                PostFormData.Content = post.Content;
                PostFormData.Id = post.Id;
                PostFormData.PublishDate = post.PublishDate;
                PostFormData.Published = post.Published ?? false;
                PostFormData.Subtitle = post.Subtitle;
                PostFormData.Title = post.Title;
                PostFormData.BlogId = Blog.Id;
            }
            IsEdit = id.HasValue;
            EditContext = new EditContext(PostFormData);
            EditContext.OnFieldChanged += HandleFieldChanged;
        }

        public void Dispose()
        {
            EditContext.OnFieldChanged -= HandleFieldChanged;
        }
    }
}
