﻿using System;
using System.Collections.Generic;

namespace Blogs.Jhray.Persistence.Database.Entities
{
    public partial class Posts
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public string Content { get; set; }
        public DateTime? PublishDate { get; set; }
        public bool? Published { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool? TopPost { get; set; }
        public string DirectLink { get; set; }

        public long BlogId { get; set; }
        public Blog Blog { get; set; }

        public List<Comment> Comments { get; set; }
    }
}
