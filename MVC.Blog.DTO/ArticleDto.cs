﻿using System;

namespace MVC.Blog.DTO
{
    /// <summary>
    /// 文章
    /// </summary>
    public class ArticleDto
    {
        public Guid ArticleId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreateTime { get; set; }
        public string Email { get; set; }
        public int GoodCount { get; set; }
        public int BadCount { get; set; }
        public string ImagePath { get; set; }
        public string[] CategoryNames { get; set; }
        public Guid[] CategoryIds { get; set; }
    }
}