// Filename: MatchReference.cs
// Date Created: 2019-06-01
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

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public sealed class MatchReference : ValueObject
    {
        public MatchReference(string externalId)
        {
            if (string.IsNullOrWhiteSpace(externalId) || !externalId.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(externalId));
            }

            Value = externalId;
        }

        public MatchReference(long externalId)
        {
            if (externalId < 0)
            {
                throw new ArgumentException(nameof(externalId));
            }

            Value = externalId.ToString();
        }

        public MatchReference(Guid externalId)
        {
            if (externalId == Guid.Empty)
            {
                throw new ArgumentException(nameof(externalId));
            }

            Value = externalId.ToString();
        }

        public string Value { get; private set; }

        public static implicit operator MatchReference(string externalId)
        {
            return new MatchReference(externalId);
        }

        public static implicit operator MatchReference(long externalId)
        {
            return new MatchReference(externalId);
        }

        public static implicit operator MatchReference(Guid externalId)
        {
            return new MatchReference(externalId);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
