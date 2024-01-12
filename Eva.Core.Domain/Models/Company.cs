﻿using Eva.Core.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.Domain.Models
{
    public class Company : DomainObject
    {
        public string Name { get; set; }
        public List<Department> Departments { get; set; }
    }
}
