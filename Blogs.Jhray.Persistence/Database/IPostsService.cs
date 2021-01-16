using Blogs.Jhray.Persistence.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Jhray.Persistence.Database
{
    public interface IPostsService
    {
        long GetLatestPostId(long blogId = 1);
        IEnumerable<long> ListPostIds();
        long GetRandomPostId();
        Posts FindPost(long id);
        IEnumerable<Posts> ListPosts();
    }
}
