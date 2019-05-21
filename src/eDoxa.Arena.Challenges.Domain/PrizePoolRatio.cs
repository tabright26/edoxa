// Filename: PrizePoolRatio.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Globalization;

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain
{
    public class PrizePoolRatio : TypeObject<PrizePoolRatio, float>
    {
        public const float Min = 0.05F;
        public const float Max = 0.25F;
        public const float Default = 0.1F;

        public static readonly PrizePoolRatio MinValue = new PrizePoolRatio(Min);
        public static readonly PrizePoolRatio MaxValue = new PrizePoolRatio(Max);
        public static readonly PrizePoolRatio DefaultValue = new PrizePoolRatio(Default);

        public PrizePoolRatio(float prizePoolRatio) : base(prizePoolRatio)
        {
            if (prizePoolRatio < Min || prizePoolRatio > Max)
            {
                throw new ArgumentException(nameof(prizePoolRatio));
            }
        }

        public override string ToString()
        {
            return Value.ToString(CultureInfo.InvariantCulture);
        }
    }
}
