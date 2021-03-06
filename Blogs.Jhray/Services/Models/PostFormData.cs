﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Blogs.Jhray.Services.Models
{
    public class PostFormData
    {
        public long Id { get; set; }

        [Required]
        public long BlogId { get; set; }

        [Required]
        [StringLength(maximumLength: 999, ErrorMessage = "Try again bucko!", MinimumLength = 5)]
        public string Title { get; set; }
        
        [Required]
        [StringLength(maximumLength: 999, ErrorMessage = "Try again bucko!", MinimumLength = 5)]
        public string Subtitle { get; set; }
        
        [Required]
        public DateTime? PublishDate { get; set; }
        
        [Required]
        public bool Published { get; set; }
        
        [Required]
        [StringLength(maximumLength: 100_000, ErrorMessage = "Try again bucko!", MinimumLength = 5)]
        public string Content { get; set; }
    }
}
