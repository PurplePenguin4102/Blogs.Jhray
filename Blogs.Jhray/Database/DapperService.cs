using Blogs.Jhray.Database.Entities;
using Dapper;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Database
{
    public class DapperService
    {
        private string _cn;

        private readonly string _getPost = "SELECT * FROM posts WHERE id = @Id";
        public DapperService(string cn)
        {
            _cn = cn;
        }

        public long GetRandomPostId()
        {
            using (var conn = new NpgsqlConnection(_cn))
            {
                conn.Open();
                return conn.QuerySingle<long>("SELECT id FROM posts ORDER BY RANDOM() LIMIT 1");
            }
        }

        public Posts FindPost(long id)
        {
            using (var conn = new NpgsqlConnection(_cn))
            {
                conn.Open();
                return conn.QuerySingle<Posts>(_getPost, new { id });
            }
        }
    }
}
