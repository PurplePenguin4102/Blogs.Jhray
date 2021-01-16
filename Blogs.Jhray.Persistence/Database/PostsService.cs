using Blogs.Jhray.Persistence.Database.Entities;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Jhray.Persistence.Database
{
    public class PostsService : IPostsService
    {
        private readonly IDapperService<long> _dapperServiceIds;
        private readonly IDapperService<Posts> _dapperServicePosts;
        private readonly string _getPost = "SELECT * FROM posts WHERE id = @Id";
        private readonly string _getPosts = "SELECT * FROM posts";
        private readonly string _randomPostId = "SELECT id FROM posts ORDER BY RANDOM() LIMIT 1";
        private readonly string _latestPostId = @"SELECT id FROM posts WHERE ""BlogId"" = @blogId ORDER BY publish_date DESC LIMIT 1";
        private readonly string _allPostId = "SELECT id FROM posts ORDER BY publish_date";

        public PostsService(IDapperService<Posts> dapperServicePosts, IDapperService<long> dapperServiceIds)
        {
            _dapperServiceIds = dapperServiceIds;
            _dapperServicePosts = dapperServicePosts;
        }        

        public long GetLatestPostId(long blogId = 1) => _dapperServiceIds.QuerySingle(_latestPostId, new { blogId });

        public IEnumerable<long> ListPostIds() => _dapperServiceIds.Query(_allPostId);

        public long GetRandomPostId() => _dapperServiceIds.QuerySingle(_randomPostId);

        public Posts FindPost(long id) => _dapperServicePosts.QuerySingle(_getPost, new { id });

        public IEnumerable<Posts> ListPosts() => _dapperServicePosts.Query(_getPosts);

    }
}
