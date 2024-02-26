using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Eva.Core.Domain.BaseModels
{
    public class Enumeration : IComparable
    {
        public int Id { get; set; }
        public string Name { get; set; }
        protected Enumeration(int id, string name)
        {
            Id = id;
            Name = name;
        }
        public override string ToString()
        {
            return Name;
        }
        public static IEnumerable<T> GetAll<T>() where T : Enumeration =>
        typeof(T).GetFields(BindingFlags.Public |
                            BindingFlags.Static |
                            BindingFlags.DeclaredOnly)
                 .Select(f => f.GetValue(null))
                 .Cast<T>();

        public override bool Equals(object obj)
        {
            if (obj is not Enumeration otherValue)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }
        public string GetName<T>() where T : Enumeration, new()
        { 
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var info in fields)
            {
                var instance = new T();
                var locatedValue = info.GetValue(instance) as T;
                if (locatedValue?.Name == Name)
                {
                    return info.Name;
                }
            }
            throw new Exception($"The enumeration value {Name} could not be found");
        }
        public int CompareTo(object other) => Id.CompareTo(((Enumeration)other).Id);
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
