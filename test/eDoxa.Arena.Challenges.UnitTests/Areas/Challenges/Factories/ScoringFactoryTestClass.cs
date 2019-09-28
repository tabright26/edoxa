// Filename: ScoringFactoryTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Strategies;
using eDoxa.Arena.Challenges.UnitTests.TestHelpers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Factories
{
    public sealed class ScoringFactoryTestClass : UnitTestClass
    {
        public ScoringFactoryTestClass(TestDataFixture testData) : base(testData)
        {
        }

        public static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game}).ToList();

        public static IEnumerable<object[]> ScoringStrategyDataSets =>
            Assembly.GetAssembly(typeof(Startup))
                .GetTypes()
                .Where(type => typeof(IScoringStrategy).IsAssignableFrom(type) && type.IsInterface == false)
                .Select(type => Activator.CreateInstance(type) as IScoringStrategy)
                .Select(strategy => new object[] {strategy})
                .ToList();

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
        public void CreateInstance_FromReflection_ShouldBeGame(ChallengeGame game)
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
            var action = new Action(() => scoringFactory.CreateInstance(ChallengeGame.LeagueOfLegends));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}
