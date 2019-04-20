// Filename: PayoutRatio.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public partial class PayoutRatio : ValueObject
    {
        internal const float MinPayoutRatio = 0.25F;
        internal const float MaxPayoutRatio = 0.75F;
        internal const float DefaultPrimitive = 0.5F;

        public static readonly PayoutRatio Default = new PayoutRatio(DefaultPrimitive);

        private readonly float _payoutRatio;

        public PayoutRatio(float payoutRatio, bool validate = true)
        {
            if (validate)
            {
                if (payoutRatio < MinPayoutRatio ||
                    payoutRatio > MaxPayoutRatio ||
                    (decimal) payoutRatio % 0.05M != 0)
                {
                    throw new ArgumentException(nameof(payoutRatio));
                }
            }

            _payoutRatio = payoutRatio;
        }

        public static implicit operator float(PayoutRatio payoutRatio)
        {
            return payoutRatio._payoutRatio;
        }
    }

    public partial class PayoutRatio : IComparable, IComparable<PayoutRatio>
    {
        public int CompareTo([CanBeNull] object obj)
        {
            return this.CompareTo(obj as PayoutRatio);
        }

        public int CompareTo([CanBeNull] PayoutRatio other)
        {
            return _payoutRatio.CompareTo(other?._payoutRatio);
        }
    }
}