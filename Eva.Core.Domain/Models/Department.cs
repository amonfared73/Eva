using Eva.Core.Domain.BaseModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.Domain.Models
{
    public class Department : DomainObject
    {
        public string Name { get; set; }
        public List<Employee> Employees { get; set; }
    }
}
