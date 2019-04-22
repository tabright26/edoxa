// Filename: BestOfRandom.cs
// Date Created: 2019-04-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class BestOfRandom : IRandom<BestOf, BestOfRange>
    {
        private static readonly Random Random = new Random();

        public BestOf Next(BestOfRange range)
        {
            return new BestOf(Random.Next(range.MinValue, range.MaxValue + 1));
        }
    }
}