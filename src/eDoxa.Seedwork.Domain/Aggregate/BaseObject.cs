// Filename: BaseObject.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Reflection;

using eDoxa.Seedwork.Domain.Reflexion;
using JetBrains.Annotations;

namespace eDoxa.Seedwork.Domain.Aggregate
{
    public abstract class BaseObject
    {
        private const int HashMultiplier = 31;

        private static readonly IDomainSignatureCache DomainSignatureCache = new DomainSignatureCache();

        public override bool Equals([CanBeNull] object obj)
        {
            if (!(obj is BaseObject other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return this.GetType() == other.GetType() && this.EquivalentSignature(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var signatureProperties = this.SignatureProperties();

                if (!signatureProperties.Any())
                {
                    return base.GetHashCode();
                }

                var hashCode = this.GetType().GetHashCode();

                return signatureProperties.Select(propertyInfo => propertyInfo.GetValue(this, null))
                                          .Where(value => value != null)
                                          .Aggregate(hashCode, (current, value) => current * HashMultiplier ^ value.GetHashCode());
            }
        }

        protected abstract PropertyInfo[] TypeSignatureProperties();

        protected bool EquivalentSignature(BaseObject other)
        {
            var signatureProperties = this.SignatureProperties();

            var otherSignatureProperties = other.SignatureProperties();

            var enumerator = signatureProperties.GetEnumerator();

            var otherEnumerator = otherSignatureProperties.GetEnumerator();

            while (enumerator.MoveNext() && otherEnumerator.MoveNext())
            {
                var propertyInfo = enumerator.Current as PropertyInfo;

                var otherPropertyInfo = otherEnumerator.Current as PropertyInfo;

                var value = propertyInfo?.GetValue(this, null);

                var otherValue = otherPropertyInfo?.GetValue(other, null);

                if (value is null ^ otherValue is null)
                {
                    return false;
                }

                if (value != null && !value.Equals(otherValue))
                {
                    return false;
                }
            }

            return !enumerator.MoveNext() && !otherEnumerator.MoveNext();
        }

        protected PropertyInfo[] SignatureProperties()
        {
            var ownerType = this.GetType();

            var signature = DomainSignatureCache.Find(ownerType) ??
                            DomainSignatureCache.GetOrAdd(ownerType, type => new DomainSignature(type, this.TypeSignatureProperties()));

            return signature.Properties;
        }
    }
}