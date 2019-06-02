// Filename: MatchExternalId.cs
// Date Created: 2019-05-20
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
    public sealed class MatchExternalId : ValueObject
    {
        public MatchExternalId(string externalId)
        {
            if (string.IsNullOrWhiteSpace(externalId) || !externalId.All(c => char.IsLetterOrDigit(c) || c == '-' || c == '_'))
            {
                throw new ArgumentException(nameof(externalId));
            }

            Value = externalId;
        }

        public MatchExternalId(long externalId)
        {
            if (externalId < 0)
            {
                throw new ArgumentException(nameof(externalId));
            }

            Value = externalId.ToString();
        }

        public MatchExternalId(Guid externalId)
        {
            if (externalId == Guid.Empty)
            {
                throw new ArgumentException(nameof(externalId));
            }

            Value = externalId.ToString();
        }

        public string Value { get; private set; }

        public static implicit operator MatchExternalId(string externalId)
        {
            return new MatchExternalId(externalId);
        }

        public static implicit operator MatchExternalId(long externalId)
        {
            return new MatchExternalId(externalId);
        }

        public static implicit operator MatchExternalId(Guid externalId)
        {
            return new MatchExternalId(externalId);
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
