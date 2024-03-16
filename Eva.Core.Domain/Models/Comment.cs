﻿using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class Comment : DomainObject
    {
        public string Text { get; set; } = string.Empty;
        public int PostId { get; set; }
        public Post Post { get; set; }
    }
}