using System;
using System.Collections.Generic;
using System.Text;

namespace Blogs.Jhray.Persistence.Database
{
    public interface IDapperService<T>
    {
        T QuerySingle(string query, object param = null);
        IEnumerable<T> Query(string query, object param = null);
    }
}
