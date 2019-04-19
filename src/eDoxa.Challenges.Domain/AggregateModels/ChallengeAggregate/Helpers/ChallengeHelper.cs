// Filename: ChallengeHelper.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers
{
    public sealed class ChallengeHelper
    {
        public int PayoutEntries(int entries, float payoutEntries)
        {
            return Convert.ToInt32(Math.Floor(entries * payoutEntries));
        }

        public decimal PrizePool(int entries, decimal entryFee, float serviceChargeRatio)
        {
            return Math.Floor(entries * entryFee * (1 - Convert.ToDecimal(serviceChargeRatio)));
        }
    }
}