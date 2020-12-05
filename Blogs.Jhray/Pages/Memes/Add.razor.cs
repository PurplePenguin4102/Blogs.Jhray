using Blogs.Jhray.Data;
using Blogs.Jhray.Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Pages.Memes
{
    public class AddBase : ComponentBase
    {
        [Inject]
        public BlogService BlogService { get; set; }

        protected MemesBase Memes { get; set; }
        protected PostFormData PostFormData { get; set; } = new PostFormData();
        protected bool FormInvalid { get; set; } = true;
        protected EditContext EditContext;

        protected bool IsDirty => EditContext.IsModified();
        protected bool IsEdit { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        protected override void OnInitialized()
        {
            EditContext = new EditContext(PostFormData);
            EditContext.OnFieldChanged += HandleFieldChanged;
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
            await BlogService.AddPost(PostFormData);
            Memes.ReloadPosts();
            await ReinitializeForm();
        }

        protected async Task HandleValidEdit()
        {
            await BlogService.EditPost(PostFormData);
            Memes.ReloadPosts();
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
