﻿// Filename: ValueObject.cs
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

using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract class ValueObject
    {
        public static bool operator ==([CanBeNull] ValueObject left, [CanBeNull] ValueObject right)
        {
            return !(left is null ^ right is null) && (left is null || left.Equals(right));
        }

        public static bool operator !=([CanBeNull] ValueObject left, [CanBeNull] ValueObject right)
        {
            return !(left == right);
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public sealed override bool Equals([CanBeNull] object obj)
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
    }
}
