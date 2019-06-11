// Filename: MatchStatsFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

using Moq;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class MatchStatsFaker : CustomFaker<MatchStats>
    {
        public MatchStatsFaker()
        {
            this.RuleSet(
                Game.LeagueOfLegends.ToString(),
                ruleSet =>
                {
                    this.CustomInstantiator(
                        faker => new MatchStats(
                            new
                            {
                                Kills = faker.Random.Int(0, 40),
                                Deaths = faker.Random.Int(0, 15),
                                Assists = faker.Random.Int(0, 50),
                                TotalDamageDealtToChampions = faker.Random.Int(10000, 500000),
                                TotalHeal = faker.Random.Int(10000, 350000)
                            }
                        )
                    );
                }
            );
        }

        public IMatchStats FakeMatchStats(Game game)
        {
            var matchStats = this.Generate(game.ToString());

            Console.WriteLine(matchStats.DumbAsJson());

            return matchStats;
        }

        public IMatchStatsAdapter FakeMatchStatsAdapter(Game game)
        {
            var mock = new Mock<IMatchStatsAdapter>();

            var matchStats = this.FakeMatchStats(game);

            mock.SetupGet(adapter => adapter.MatchStats).Returns(matchStats);

            return mock.Object;
        }
    }
}
