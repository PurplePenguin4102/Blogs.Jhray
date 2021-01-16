using System;
using System.Collections.Generic;

namespace Blogs.Jhray.Persistence.Database.Entities
{
    public partial class ArInternalMetadata
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
