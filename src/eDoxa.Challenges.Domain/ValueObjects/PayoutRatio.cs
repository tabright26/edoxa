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

namespace eDoxa.Challenges.Domain.ValueObjects
{
    public class PayoutRatio : ValueObject
    {
        internal const float MinPayoutRatio = 0.25F;
        internal const float MaxPayoutRatio = 0.75F;

        private static readonly PayoutRatio Default = new PayoutRatio(0.5F);

        private readonly float _payoutRatio;

        public PayoutRatio(float payoutRatio)
        {
            if (payoutRatio < MinPayoutRatio ||
                payoutRatio > MaxPayoutRatio ||
                (decimal) payoutRatio % 0.05M != 0)
            {
                throw new ArgumentException(nameof(payoutRatio));
            }

            _payoutRatio = payoutRatio;
        }

        public float ToSingle()
        {
            return _payoutRatio;
        }
    }
}