using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Persistence.Database.Entities
{
    public class Blog
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string LeadIn { get; set; }
        public string Author { get; set; }
        public string HomeUrl { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }

        List<Posts> Posts { get; set; }
    }
}
