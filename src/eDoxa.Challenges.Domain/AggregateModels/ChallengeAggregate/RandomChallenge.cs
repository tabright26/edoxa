// Filename: RandomChallenge.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class RandomChallenge : Challenge
    {
        internal RandomChallenge(Game game, ChallengeName name, PublisherInterval interval) : base(game, name, new RandomChallengeSetup(interval))
        {
        }
    }
}