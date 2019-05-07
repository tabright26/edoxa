// Filename: BestOfRandom.cs
// Date Created: 2019-05-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.Random.Ranges;

namespace eDoxa.Challenges.Domain.Entities.Random
{
    public sealed class BestOfRandom : IRandom<BestOf, BestOfRange>
    {
        private static readonly System.Random Random = new System.Random();

        public BestOf Next(BestOfRange range)
        {
            return new BestOf(Random.Next(range.MinValue, range.MaxValue + 1));
        }
    }
}