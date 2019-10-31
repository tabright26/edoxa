// Filename: ScoringFactoryTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Reflection;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Domain.Strategies;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;
using eDoxa.Seedwork.Domain.Miscs;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Factories
{
    public sealed class ScoringFactoryTest : UnitTest
    {
        public ScoringFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        public static TheoryData<Game> GameDataSets =>
            new TheoryData<Game>
            {
                Game.LeagueOfLegends
            };

        public static TheoryData<IScoringStrategy> ScoringStrategyDataSets
        {
            get
            {
                var data = new TheoryData<IScoringStrategy>();

                foreach (var strategy in Assembly.GetAssembly(typeof(Startup))
                    .GetTypes()
                    .Where(type => typeof(IScoringStrategy).IsAssignableFrom(type) && type.IsInterface == false)
                    .Select(type => Activator.CreateInstance(type) as IScoringStrategy)
                    .ToList())
                {
                    data.Add(strategy);
                }

                return data;
            }
        }

        [Theory]
        [MemberData(nameof(ScoringStrategyDataSets))]
        public void CreateInstance_FromDependencyInjection_ShouldBeStrategy(IScoringStrategy strategy)
        {
            // Arrange
            var scoringFactory = new ScoringFactory(new[] {strategy});

            // Act
            var scoringStrategy = scoringFactory.CreateInstance(strategy.Game);

            // Assert
            scoringStrategy.Should().Be(strategy);
        }

        [Theory]
        [MemberData(nameof(GameDataSets))]
        public void CreateInstance_FromReflection_ShouldBeGame(Game game)
        {
            // Arrange
            var scoringFactory = new ScoringFactory();

            // Act
            var scoringStrategy = scoringFactory.CreateInstance(game);

            // Assert
            scoringStrategy.Game.Should().Be(game);
        }

        [Fact]
        public void CreateInstance_WithoutStrategy_ShouldThrowNotSupportedException()
        {
            // Arrange
            var scoringFactory = new ScoringFactory(Array.Empty<IScoringStrategy>());

            // Act
            var action = new Action(() => scoringFactory.CreateInstance(Game.LeagueOfLegends));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}
