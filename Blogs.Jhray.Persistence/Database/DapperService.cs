using Blogs.Jhray.Persistence.Database.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Persistence.Database
{
    public class DapperService<T> : IDapperService<T>
    {
        private readonly string _cn;

        public DapperService(IConfiguration cn)
        {
            _cn = cn.GetConnectionString("DefaultConnection");
        }

        public T QuerySingle(string query, object param = null)
        {
            using var conn = new NpgsqlConnection(_cn);
            conn.Open();
            return conn.QuerySingle<T>(query, param);
        }

        public IEnumerable<T> Query(string query, object param = null)
        {
            using var conn = new NpgsqlConnection(_cn);
            conn.Open();
            return conn.Query<T>(query, param);
        }


    }
}
