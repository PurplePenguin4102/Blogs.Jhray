using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Pages.ManageBlogs
{
    public class AddBlogModel
    {
        public long Id { get; set; }
        public string Author { get; set; }
        public string HomeUrl { get; set; }
        public string LeadIn { get; set; }
        public string Title { get; set; }
    }
}
