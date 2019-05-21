// Filename: PayoutRatio.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public class PayoutRatio : TypeObject<PayoutRatio, float>
    {
        public const float Min = 0.25F;
        public const float Max = 0.75F;
        public const float Default = 0.5F;

        public static readonly PayoutRatio MinValue = new PayoutRatio(Min);
        public static readonly PayoutRatio MaxValue = new PayoutRatio(Max);
        public static readonly PayoutRatio DefaultValue = new PayoutRatio(Default);

        public PayoutRatio(float payoutRatio, bool validate = true) : base(payoutRatio)
        {
            if (validate)
            {
                if (payoutRatio < Min || payoutRatio > Max || (decimal) payoutRatio % 0.05M != 0)
                {
                    throw new ArgumentException(nameof(payoutRatio));
                }
            }
        }

        public override string ToString()
        {
            return Value.ToString("R");
        }
    }
}
