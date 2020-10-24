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
    }
}
