﻿using System.Collections.Generic;

namespace ProgParty.BoredPanda.Api.Result
{
    public class ArticleResult
    {
        public string Url { get; set; }

        public List<string> ImageUrls { get; set; } 
        public string Title { get; internal set; }
        public string Content { get; internal set; }
        public string ViewsCount { get; internal set; }
        public string AuthorTime { get; internal set; }
    }
}
