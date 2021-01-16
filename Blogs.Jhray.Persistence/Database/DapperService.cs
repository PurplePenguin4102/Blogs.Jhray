using Blogs.Jhray.Persistence.Database.Entities;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Persistence.Database
{
    public class DapperService
    {
        private readonly string _cn;

        private readonly string _getPost = "SELECT * FROM posts WHERE id = @Id";
        private readonly string _getPosts = "SELECT * FROM posts";
        private readonly string _randomPostId = "SELECT id FROM posts ORDER BY RANDOM() LIMIT 1";
        private readonly string _latestPostId = "SELECT id FROM posts ORDER BY publish_date DESC LIMIT 1";
        private readonly string _allPostId = "SELECT id FROM posts ORDER BY publish_date";
        public DapperService(string cn)
        {
            _cn = cn;
        }

        public long GetLatestPostId()
        {
            using var conn = new NpgsqlConnection(_cn);
            conn.Open();
            return conn.QuerySingle<long>(_latestPostId);
        }

        public IEnumerable<long> ListPostIds()
        {
            using var conn = new NpgsqlConnection(_cn);
            conn.Open();
            return conn.Query<long>(_allPostId);
        }

        public long GetRandomPostId()
        {
            using var conn = new NpgsqlConnection(_cn);
            conn.Open();
            return conn.QuerySingle<long>(_randomPostId);
        }

        public Posts FindPost(long id)
        {
            using var conn = new NpgsqlConnection(_cn);
            conn.Open();
            return conn.QuerySingle<Posts>(_getPost, new { id });
        }

        public List<Posts> ListPosts()
        {
            using var conn = new NpgsqlConnection(_cn);
            conn.Open();
            return conn.Query<Posts>(_getPosts).ToList();
        }
    }
}
