// Filename: WinnerPrize.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Challenges.Domain
{
    public sealed class FirstPrize : Prize
    {
        public FirstPrize(PrizePool prizePool, PrizePoolRatio prizePoolRatio) : base(prizePool * prizePoolRatio)
        {
        }

        public FirstPrize(PrizePool prizePool) : base(prizePool * PrizePoolRatio.DefaultValue)
        {
        }

        internal FirstPrize(decimal prize) : base(prize)
        {
        }
    }
}