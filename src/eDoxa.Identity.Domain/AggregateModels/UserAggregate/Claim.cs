// Filename: Claim.cs
// Date Created: 2019-07-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Identity.Domain.AggregateModels.UserAggregate
{
    public sealed class Claim : ValueObject
    {
        public Claim(string type, string value)
        {
            Type = type;
            Value = value;
        }

        public string Type { get; }

        public string Value { get; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Type;
            yield return Value;
        }

        public override string ToString()
        {
            return $"{Type}={Value}";
        }
    }
}
