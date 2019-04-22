// Filename: WinnerPrize.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public class WinnerPrize : Prize
    {
        public WinnerPrize(PrizePool prizePool, PrizePoolRatio prizePoolRatio) : base(prizePool * prizePoolRatio)
        {
        }

        public WinnerPrize(PrizePool prizePool) : base(prizePool * PrizePoolRatio.DefaultValue)
        {
        }

        internal WinnerPrize(decimal prize) : base(prize)
        {
        }
    }
}