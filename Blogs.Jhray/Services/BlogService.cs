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
        private readonly IPostsService _postsService;
        public BlogService(BlogContext blogContext, IPostsService postsService)
        {
            _blogContext = blogContext;
            _postsService = postsService;
        }

        internal Blog GetBlogMetadata(string homeUrl)
        {
            var retVal = _blogContext.Blogs.Where(b => b.HomeUrl == homeUrl);
            if (retVal.Any())
            {
                return retVal.First();
            }
            throw new KeyNotFoundException();
        }

        internal List<Blog> ListBlogs()
        {
            var retVal = _blogContext.Blogs.ToList();
            return retVal;
        }

        internal long GetRandomPostId()
        {
            var post = _postsService.GetRandomPostId();
            return post;
        }

        internal long GetLatestPostId(long blogId = 1)
        {
            var post = _postsService.GetLatestPostId(blogId);
            return post;
        }

        internal List<long> ListPostIds()
        {
            var posts = _postsService.ListPostIds();
            return posts.ToList();
        }

        public List<Posts> ListPosts(long blogId = 1)
        {
            var retVal = _blogContext.Posts.Where(b => b.BlogId == blogId).ToList();
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
            var post = _postsService.FindPost(id);
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
