﻿using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using Blogs.Jhray.Data;
using System.Threading.Tasks;
using Markdig;
using Blogs.Jhray.Database.Entities;

namespace Blogs.Jhray.Pages
{
    public class FetchDataBase : ComponentBase
    {
        protected List<Posts> blogPosts;

        [Inject]
        public BlogService BlogService { get; set; }
        public FetchDataBase(){}

        protected override async Task OnInitializedAsync()
        {
            blogPosts =  BlogService.ListPosts().ToList();
            await Task.CompletedTask;
        }
    }
}
