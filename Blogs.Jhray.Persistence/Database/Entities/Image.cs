using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Persistence.Database.Entities
{
    public class Image
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ToolTip { get; set; }
        public string Author { get; set; }
        public string DirectUrl { get; set; }
        public string FileLocation { get; set; }
        public long FileSize { get; set; }
        public string FileCheckSum { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
