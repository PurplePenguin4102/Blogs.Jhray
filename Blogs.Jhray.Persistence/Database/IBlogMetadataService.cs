using Blogs.Jhray.Persistence.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Jhray.Persistence.Database
{
    public interface IBlogMetadataService
    {
        IEnumerable<Blog> ListBlogs();
    }
}
