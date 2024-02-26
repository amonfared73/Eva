using System.Reflection;

namespace Eva.Core.Domain.BaseModels
{
    public class Enumeration : IComparable
    {
        private readonly int _id;
        private readonly string _value;
        public int Id { get { return _id; } }
        public string Value { get { return _value; } }
        protected Enumeration()
        {

        }
        protected Enumeration(int id, string value)
        {
            _id = id;
            _value = value;
        }
        public int CompareTo(object? obj)
        {
            return Id.CompareTo(((Enumeration)obj).Value);
        }
        public override string ToString()
        {
            return Value;
        }
        public override int GetHashCode()
        {
            return _id.GetHashCode();
        }
        public override bool Equals(object? obj)
        {
            var otherVlue = obj as Enumeration;

            if (otherVlue == null)
                return false;

            var typeMatches = GetType().Equals(obj.GetType());
            var idMatches = _id.Equals(otherVlue.Id);
            var valueMatches = _value.Equals(otherVlue.Value);

            return typeMatches && idMatches && valueMatches;
        }
        public static IEnumerable<T> GetAll<T>() where T : Enumeration, new()
        {
            var type = typeof(T);
            var fields = type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            foreach (var field in fields)
            {
                var instance = new T();
                var locatedValue = field.GetValue(instance) as T;
                if (locatedValue != null)
                    yield return locatedValue;
            }
        }
        public static T FromId<T>(int id) where T : Enumeration, new()
        {
            var matchingItem = parse<T, int>(id, "id", item => item.Id == id);
            return matchingItem;
        }

        public static T FromValue<T>(string value) where T : Enumeration, new()
        {
            var matchingItem = parse<T, string>(value, "value", item => item.Value == value);
            return matchingItem;
        }

        private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = GetAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }
    }
}
