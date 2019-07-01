// Filename: ScoringFactoryTest.cs
// Date Created: 2019-06-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Strategies;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Factories
{
    [TestClass]
    public sealed class ScoringFactoryTest
    {
        private static IEnumerable<object[]> DataChallengeGames => ChallengeGame.GetEnumerations().Select(game => new object[] {game}).ToList();

        private static IEnumerable<object[]> DataScoringStrategies =>
            Assembly.GetAssembly(typeof(Startup))
                .GetTypes()
                .Where(type => typeof(IScoringStrategy).IsAssignableFrom(type) && type.IsInterface == false)
                .Select(type => Activator.CreateInstance(type) as IScoringStrategy)
                .Select(strategy => new object[] {strategy})
                .ToList();

        [DataTestMethod]
        [DynamicData(nameof(DataScoringStrategies))]
        public void CreateInstance_FromDependencyInjection_ShouldBeStrategy(IScoringStrategy strategy)
        {
            // Arrange
            var scoringFactory = new ScoringFactory(new[] {strategy});

            // Act
            var scoringStrategy = scoringFactory.CreateInstance(strategy.Game);

            // Assert
            scoringStrategy.Should().Be(strategy);
        }

        [DataTestMethod]
        [DynamicData(nameof(DataChallengeGames))]
        public void CreateInstance_FromReflection_ShouldBeGame(ChallengeGame game)
        {
            // Arrange
            var scoringFactory = new ScoringFactory();

            // Act
            var scoringStrategy = scoringFactory.CreateInstance(game);

            // Assert
            scoringStrategy.Game.Should().Be(game);
        }

        [TestMethod]
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
