﻿using System;

namespace MVC.Blog.DTO
{
    public class UserInformationDto
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public string ImagePath { get; set; }
        public string SiteName { get; set; }
        public int FansCount { get; set; }
        public int FocusCount { get; set; }
    }
}