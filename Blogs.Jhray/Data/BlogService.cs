using Blogs.Jhray.Data.Models;
using Blogs.Jhray.Database;
using Blogs.Jhray.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;

namespace Blogs.Jhray.Data
{
    public class BlogService
    {
        private readonly BlogContext _blogContext;
        public BlogService(BlogContext blogContext)
        {
            _blogContext = blogContext;
        }

        public List<Posts> ListPosts()
        {
            return _blogContext.Posts.ToList();
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
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };
            _blogContext.Posts.Add(newPost);
            await _blogContext.SaveChangesAsync();
            return newPost.Id;
        }

        internal List<Posts> GetPost(long showId)
        {
            return new List<Posts> { _blogContext.Posts.Find(showId) };
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
            
            await _blogContext.SaveChangesAsync();
            return post.Id;
        }

        internal async Task<Posts> Find(long id)
        {
            return await _blogContext.Posts.FindAsync(id);
        }
    }
}
