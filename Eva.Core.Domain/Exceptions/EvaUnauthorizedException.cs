using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.Domain.Exceptions
{
    public class EvaUnauthorizedException : Exception
    {
        public EvaUnauthorizedException(string? message) : base(message)
        {
        }
    }
}
