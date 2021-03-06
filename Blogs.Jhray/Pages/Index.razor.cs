﻿using System;
using System.Collections.Generic;
using System.Linq;
using Blogs.Jhray.Services;
using Blogs.Jhray.Pages.BlogContent;
using Microsoft.AspNetCore.Components;

namespace Blogs.Jhray.Pages
{
    public partial class Index : ComponentBase
    {
        protected BlogContainer BlogContainer { get; set; }
        protected long LatestPostId { get; set; } = -1;
        private int _currentPostIdx;
        private List<long> _postIds;
        
        protected enum GetPostOpt
        {
            Random,
            First,
            Previous,
            Next,
            Last
        }

        [Inject]
        protected BlogService Blogs { get; set; }

        protected override void OnInitialized()
        {
            _postIds = Blogs.ListPostIds();
            LatestPostId = _postIds.Last();
            _currentPostIdx = _postIds.Count - 1;
        }

        protected void GetPost(GetPostOpt directive)
        {
            var rnd = new Random();
            switch (directive)
            {
                case GetPostOpt.First: _currentPostIdx = 1; break;
                case GetPostOpt.Previous: _currentPostIdx--; break;
                case GetPostOpt.Next: _currentPostIdx++; break;
                case GetPostOpt.Random: _currentPostIdx = rnd.Next(0, _postIds.Count); break;
                case GetPostOpt.Last:
                default: _currentPostIdx = _postIds.Count - 1; break;
            }
            _currentPostIdx = _currentPostIdx > _postIds.Count - 1 ? _postIds.Count - 1 : _currentPostIdx;
            _currentPostIdx = _currentPostIdx < 1 ? 1 : _currentPostIdx;
            BlogContainer.GetPostById(_postIds[_currentPostIdx]);
        }
    }
}
