﻿using Eva.Core.Domain.BaseModels;
using System.Text.Json.Serialization;

namespace Eva.Core.Domain.Models
{
    public class User : DomainObject
    {
        public string Username { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public bool IsAdmin { get; set; }
        [JsonIgnore]
        public ICollection<UserRoleMapping> UserRoleMapping { get; set; }
        public override string ToString() => Username;
    }
}