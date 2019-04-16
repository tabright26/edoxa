// Filename: ChallengePubliserFactory.cs
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
    public sealed class ChallengePubliserFactory
    {
        private static readonly Lazy<ChallengePubliserFactory> Lazy = new Lazy<ChallengePubliserFactory>(() => new ChallengePubliserFactory());

        public static ChallengePubliserFactory Instance
        {
            get
            {
                return Lazy.Value;
            }
        }

        public IChallengePublisherStrategy Create(ChallengePublisherPeriodicity periodicity, Game game)
        {
            switch (game)
            {
                case Game.LeagueOfLegends:

                    var factory = LeagueOfLegendsChallengePublisherFactory.Instance;

                    return factory.Create(periodicity);

                default:

                    throw new NotImplementedException();
            }
        }
    }
}