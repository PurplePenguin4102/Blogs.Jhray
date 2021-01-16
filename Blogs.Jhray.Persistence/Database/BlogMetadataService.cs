using Blogs.Jhray.Persistence.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Jhray.Persistence.Database
{
    public class BlogMetadataService : IBlogMetadataService
    {
        private readonly string _getBlogs = @"SELECT * FROM ""Blogs""";
        private readonly IDapperService<long> _dapperServiceIds;
        private readonly IDapperService<Blog> _dapperServiceBlogs;

        public BlogMetadataService(IDapperService<Blog> dapperServiceBlogs, IDapperService<long> dapperServiceIds)
        {
            _dapperServiceIds = dapperServiceIds;
            _dapperServiceBlogs = dapperServiceBlogs;
        }

        public IEnumerable<Blog> ListBlogs() => _dapperServiceBlogs.Query(_getBlogs);
    }
}
