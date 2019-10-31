// Filename: StatTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatTest : UnitTest
    {
        public StatTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static TheoryData<StatName, StatValue, StatWeighting> StatPropsDataSets
        {
            get
            {
                var data = new TheoryData<StatName, StatValue, StatWeighting>();

                var stats = new Faker().Game().Stats(Game.LeagueOfLegends);

                var factory = new ScoringFactory();

                var strategy = factory.CreateInstance(Game.LeagueOfLegends);

                var match = new StatMatch(
                    strategy.Scoring,
                    stats,
                    new GameReference(Guid.NewGuid()),
                    new UtcNowDateTimeProvider());

                foreach (var stat in match.Stats)
                {
                    data.Add(stat.Name, stat.Value, stat.Weighting);
                }

                return data;
            }
        }

        [Theory]
        [MemberData(nameof(StatPropsDataSets))]
        public void Contructor_Tests(StatName name, StatValue value, StatWeighting weighting)
        {
            // Act
            var stat = new Stat(name, value, weighting);

            // Assert
            stat.Name.Should().Be(name);
            stat.Value.Should().Be(value);
            stat.Weighting.Should().Be(weighting);
            stat.Score.Should().NotBeNull();
        }
    }
}
