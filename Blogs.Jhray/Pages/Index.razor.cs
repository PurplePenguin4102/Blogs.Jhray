using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blogs.Jhray.Data;
using Blogs.Jhray.Pages.Memes;
using Microsoft.AspNetCore.Components;

namespace Blogs.Jhray.Pages
{
    public class IndexBase : ComponentBase
    {
        protected BlogContainerBase BlogContainer { get; set; }
        protected long RandomId { get; set; } = -1;
        [Inject]
        protected BlogService Blogs { get; set; }

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                RandomId = Blogs.GetRandomPostId();
                BlogContainer.GetPostById(RandomId);
            }
        }
    }
}
