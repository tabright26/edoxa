// Filename: WinnerPrize.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels
{
    public class WinnerPrize : Prize
    {
        // TODO: Refactor to a real object.
        private const float PrizePoolRatio = 0.1F;

        public WinnerPrize(PrizePool prizePool) : base(prizePool * Convert.ToDecimal(PrizePoolRatio))
        {
        }
    }
}