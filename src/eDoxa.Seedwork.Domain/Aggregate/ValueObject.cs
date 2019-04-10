// Filename: ValueObject.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Reflection;

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

        public sealed override bool Equals(object obj)
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