// Filename: ValueObject.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract class ValueObject : BaseObject
    {
        public static bool operator ==(ValueObject left, ValueObject right)
        {
            return EqualityComparer<ValueObject>.Default.Equals(left, right);
        }

        public static bool operator !=(ValueObject left, ValueObject right)
        {
            return !(left == right);
        }

        public static IEnumerable<TValueObject> GetAll<TValueObject>()
        where TValueObject : ValueObject
        {
            return typeof(TValueObject).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                                       .Select(field => field.GetValue(null))
                                       .Where(obj => obj is TValueObject)
                                       .Cast<TValueObject>();
        }

        public sealed override bool Equals([CanBeNull] object obj)
        {
            return base.Equals(obj);
        }

        public sealed override int GetHashCode()
        {
            return base.GetHashCode();
        }

        protected sealed override PropertyInfo[] TypeSignatureProperties()
        {
            return this.GetType().GetProperties();
        }
    }
}
