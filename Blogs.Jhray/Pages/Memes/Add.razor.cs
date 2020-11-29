using Blogs.Jhray.Data;
using Blogs.Jhray.Data.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
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
        
        protected override void OnInitialized()
        {
            EditContext = new EditContext(PostFormData);
            EditContext.OnFieldChanged += HandleFieldChanged;
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
            ReinitializeForm();
        }

        private void ReinitializeForm()
        {
            EditContext.OnFieldChanged -= HandleFieldChanged;
            PostFormData = new PostFormData();
            EditContext = new EditContext(PostFormData);
            EditContext.OnFieldChanged += HandleFieldChanged;
        }

        public void Dispose()
        {
            EditContext.OnFieldChanged -= HandleFieldChanged;
        }
    }
}
