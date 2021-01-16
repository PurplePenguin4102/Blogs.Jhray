using Blogs.Jhray.Services.Models;
using Blogs.Jhray.Persistence.Database;
using Blogs.Jhray.Persistence.Database.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Blogs.Jhray.Services
{
    public class BlogService
    {
        private readonly BlogContext _blogContext;
        private readonly DapperService _dapperService;
        public BlogService(BlogContext blogContext, DapperService dapperService)
        {
            _blogContext = blogContext;
            _dapperService = dapperService;
        }

        internal long GetRandomPostId()
        {
            var post = _dapperService.GetRandomPostId();
            return post;
        }

        internal long GetLatestPostId()
        {
            var post = _dapperService.GetLatestPostId();
            return post;
        }

        internal List<long> ListPostIds()
        {
            var posts = _dapperService.ListPostIds();
            return posts.ToList();
        }

        public List<Posts> ListPosts()
        {
            var retVal = _blogContext.Posts.ToList();
            return retVal;
        }

        public async Task DeletePostById(long id)
        {
            var toRemove = _blogContext.Posts.First(p => p.Id == id);
            _blogContext.Posts.Remove(toRemove);
            await _blogContext.SaveChangesAsync();
        }

        public async Task<long> AddPost(PostFormData posts)
        {
            var newPost = new Posts()
            {
                Content = posts.Content,
                Title = posts.Title,
                Subtitle = posts.Subtitle,
                PublishDate = posts.PublishDate,
                Published = posts.Published,
                TopPost = false,
                BlogId = posts.BlogId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _blogContext.Posts.Add(newPost);
            await _blogContext.SaveChangesAsync();
            return newPost.Id;
        }

        internal Posts GetPostReadOnly(long id)
        {
            var post = _dapperService.FindPost(id);
            return post;
        }

        internal async Task<long> EditPost(PostFormData form)
        {
            var post = await _blogContext.Posts.FindAsync(form.Id);
            post.Content = form.Content;
            post.Title = form.Title;
            post.Subtitle = form.Subtitle;
            post.PublishDate = form.PublishDate;
            post.Published = form.Published;
            post.TopPost = false;
            post.UpdatedAt = DateTime.Now;
            post.BlogId = form.BlogId;
            
            await _blogContext.SaveChangesAsync();
            return post.Id;
        }

        internal async Task<Posts> Find(long id)
        {
            var retVal = await _blogContext.Posts.FindAsync(id);
            return retVal;
        }
    }
}
