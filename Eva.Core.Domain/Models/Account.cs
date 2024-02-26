﻿using Eva.Core.Domain.BaseModels;

namespace Eva.Core.Domain.Models
{
    public class Account : DomainObject
    {
        public string Name { get; set; } = string.Empty;
        public IEnumerable<Account> Accounts { get; set; }
    }
}