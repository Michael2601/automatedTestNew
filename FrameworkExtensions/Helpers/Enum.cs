using System.Collections.Generic;
using System.Linq;

#nullable enable
namespace FrameworkExtensions.Helpers
{
    public abstract class Enum
    {
        protected bool Equals(Enum other) => Equals(other as object);

        public override int GetHashCode() => base.GetHashCode();

        public override bool Equals(object? obj) => obj != null && obj.ToString() == ToString();

        public static bool operator ==(Enum first, Enum second) => Equals(first, second);

        public static bool operator !=(Enum first, Enum second) => !(first == second);

        public static IEnumerable<T?> GetValues<T>() where T : Enum
        {
            return typeof(T)
                .GetProperties()
                .Where(field => field.DeclaringType == typeof(T))
                .Select(field => field.GetValue(null) as T);
        }

        public abstract override string ToString();
    }
}
