// Filename: UserGameReference.cs
// Date Created: 2019-06-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Seedwork.Common.ValueObjects
{
    public sealed class UserGameReference : ValueObject
    {
        public UserGameReference(string providerKey)
        {
            if (string.IsNullOrWhiteSpace(providerKey) || !providerKey.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(providerKey));
            }

            ProviderKey = providerKey;
        }

        public string ProviderKey { get; private set; }

        public static implicit operator UserGameReference(string providerKey)
        {
            return new UserGameReference(providerKey);
        }

        public override string ToString()
        {
            return ProviderKey;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return ProviderKey;
        }
    }
}
