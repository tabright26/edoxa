// Filename: PayoutRatio.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels
{
    public class PayoutRatio : ValueObject
    {
        private const float DefaultValue = 0.5F;

        internal static readonly PayoutRatio Default = new PayoutRatio(DefaultValue);

        internal PayoutRatio(float payoutRatio) : this()
        {
            Value = payoutRatio;
        }

        private PayoutRatio()
        {
            // Required by EF Core.
        }

        public float Value { get; private set; }

        public static implicit operator float(PayoutRatio payoutRatio)
        {
            return payoutRatio.Value;
        }

        public override string ToString()
        {
            return Value.ToString("P2");
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }
    }
}
