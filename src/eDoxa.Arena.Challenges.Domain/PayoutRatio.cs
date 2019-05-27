// Filename: PayoutRatio.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public class PayoutRatio : TypedObject<PayoutRatio, float>
    {
        private const float DefaultValue = 0.5F;

        internal static readonly PayoutRatio Default = new PayoutRatio(DefaultValue);

        internal PayoutRatio(float payoutRatio)
        {
            Value = payoutRatio;
        }

        public override string ToString()
        {
            return Value.ToString("P2");
        }
    }
}
