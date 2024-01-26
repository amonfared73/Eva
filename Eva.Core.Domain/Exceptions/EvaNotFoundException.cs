using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.Domain.Exceptions
{
    public class EvaNotFoundException : Exception
    {
        public Type Entity { get; }

        public EvaNotFoundException(Type entity)
        {
            Entity = entity;
        }

        public EvaNotFoundException(string? message, Type entity) : base(message)
        {
            Entity = entity;
        }

        public EvaNotFoundException(string? message, Exception? innerException, Type entity) : base(message, innerException)
        {
            Entity = entity;
        }
    }
}
