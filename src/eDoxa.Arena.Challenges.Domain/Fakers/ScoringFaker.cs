// Filename: ScoringFaker.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Strategies;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.Dtos;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;

using Moq;

namespace eDoxa.Arena.Challenges.Domain.Fakers
{
    public sealed class ScoringFaker : CustomFaker<Scoring>
    {
        public ScoringFaker()
        {
            this.RuleSet(
                Game.LeagueOfLegends.ToString(),
                ruleSet =>
                {
                    this.CustomInstantiator(
                        scoring => new Scoring(
                            new HashSet<ScoringItem>
                            {
                                new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Kills)), new StatWeighting(4F)),
                                new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Deaths)), new StatWeighting(-3F)),
                                new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Assists)), new StatWeighting(3F)),
                                new ScoringItem(
                                    new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalDamageDealtToChampions)),
                                    new StatWeighting(0.00015F)
                                ),
                                new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalHeal)), new StatWeighting(0.0008F))
                            }
                        )
                    );
                }
            );
        }

        public IScoring FakeScoring(Game game)
        {
            return this.Generate(game.ToString());
        }

        public IScoringStrategy FakeScoringStrategy(Game game)
        {
            var mock = new Mock<IScoringStrategy>();

            var scoring = this.FakeScoring(game);

            mock.SetupGet(strategy => strategy.Scoring).Returns(scoring);

            return mock.Object;
        }
    }
}
