// Filename: ChallengeScoringFactory.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;

namespace eDoxa.Challenges.Domain.Services.Factories
{
    public sealed class ChallengeScoringFactory
    {
        private static readonly Lazy<ChallengeScoringFactory> _lazy = new Lazy<ChallengeScoringFactory>(() => new ChallengeScoringFactory());

        public static ChallengeScoringFactory Instance
        {
            get
            {
                return _lazy.Value;
            }
        }

        public IChallengeScoringStrategy Create(Challenge challenge)
        {
            switch (challenge.Game)
            {
                case Game.LeagueOfLegends:

                    var factory = LeagueOfLegendsChallengeScoringFactory.Instance;

                    return factory.Create(challenge);

                default:

                    throw new NotImplementedException();
            }
        }
    }
}