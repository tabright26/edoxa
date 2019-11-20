// Filename: StatTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using Bogus;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatTest : UnitTest
    {
        public StatTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static TheoryData<StatName, StatValue, StatWeighting> StatTestData
        {
            get
            {
                var faker = new Faker();

                var data = new TheoryData<StatName, StatValue, StatWeighting>();

                var scoring = new Scoring
                {
                    [new StatName("StatName1")] = new StatWeighting(0.00015F),
                    [new StatName("StatName2")] = new StatWeighting(1),
                    [new StatName("StatName3")] = new StatWeighting(0.77F),
                    [new StatName("StatName4")] = new StatWeighting(100),
                    [new StatName("StatName5")] = new StatWeighting(-3)
                };

                var stats = new Dictionary<string, double>  {
                    ["StatName1"] = faker.Random.Int(0, 40),
                    ["StatName2"] = faker.Random.Int(0, 15),
                    ["StatName3"] = faker.Random.Int(0, 50),
                    ["StatName4"] = faker.Random.Int(10000, 500000),
                    ["StatName5"] = faker.Random.Int(10000, 350000)
                };

                var match = new Match(
                    scoring.Map(stats),
                    new GameUuid(Guid.NewGuid()));

                foreach (var stat in match.Stats)
                {
                    data.Add(stat.Name, stat.Value, stat.Weighting);
                }

                return data;
            }
        }

        [Theory]
        [MemberData(nameof(StatTestData))]
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
