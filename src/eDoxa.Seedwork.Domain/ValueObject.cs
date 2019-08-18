// Filename: ValueObject.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace eDoxa.Seedwork.Domain
{
    public abstract class ValueObject
    {
        public static bool operator ==(ValueObject? left, ValueObject? right)
        {
            return !(left is null ^ right is null) && (left is null || left.Equals(right));
        }

        public static bool operator !=(ValueObject? left, ValueObject? right)
        {
            return !(left == right);
        }

        public static IEnumerable<TValueObject> GetValues<TValueObject>()
        where TValueObject : ValueObject
        {
            return typeof(TValueObject).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Select(fieldInfo => fieldInfo.GetValue(null))
                .Cast<TValueObject>()
                .ToList();
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public sealed override bool Equals(object? obj)
        {
            if (obj == null || obj.GetType() != this.GetType())
            {
                return false;
            }

            var other = (ValueObject) obj;

            using (var thisValues = this.GetAtomicValues().GetEnumerator())
            {
                using (var otherValues = other.GetAtomicValues().GetEnumerator())
                {
                    while (thisValues.MoveNext() && otherValues.MoveNext())
                    {
                        if (thisValues.Current is null ^ otherValues.Current is null)
                        {
                            return false;
                        }

                        if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                        {
                            return false;
                        }
                    }

                    return !thisValues.MoveNext() && !otherValues.MoveNext();
                }
            }
        }

        public sealed override int GetHashCode()
        {
            return this.GetAtomicValues().Select(x => x != null ? x.GetHashCode() : 0).Aggregate((x, y) => x ^ y);
        }

        public abstract override string ToString();
    }
}
