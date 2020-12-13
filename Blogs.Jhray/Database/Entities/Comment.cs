using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Blogs.Jhray.Database.Entities
{
    public class Comment
    {
        public long Id { get; set; }
        public string Content { get; set; }

        public long PostsId { get; set; }
        public Posts Post { get; set; }

        public string UserId { get; set; }
        public BlogsJhrayUser User { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
