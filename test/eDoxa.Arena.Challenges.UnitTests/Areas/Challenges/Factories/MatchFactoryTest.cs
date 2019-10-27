// Filename: MatchFactoryTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Adapters;
using eDoxa.Arena.Challenges.Api.Areas.Challenges.Factories;
using eDoxa.Arena.Challenges.Api.Temp.LeagueOfLegends.Abstractions;
using eDoxa.Arena.Challenges.Domain.Adapters;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Areas.Challenges.Factories
{
    public sealed class MatchFactoryTest : UnitTest
    {
        public MatchFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void CreateInstance_FromDependencyInjection_ShouldBeLeagueOfLegendsMatchAdapter()
        {
            // Arrange
            var mockLeagueOfLegendsProxy = new Mock<ILeagueOfLegendsService>();

            var leagueOfLegendsMatchAdapter = new LeagueOfLegendsMatchAdapter(mockLeagueOfLegendsProxy.Object);

            var matchAdapters = new List<IMatchAdapter>
            {
                leagueOfLegendsMatchAdapter
            };

            var matchFactory = new MatchFactory(matchAdapters);

            // Act
            var matchAdapter = matchFactory.CreateInstance(ChallengeGame.LeagueOfLegends);

            // Assert
            matchAdapter.Should().Be(leagueOfLegendsMatchAdapter);
        }

        [Fact]
        public void CreateInstance_WithoutAdapter_ShouldThrowNotSupportedException()
        {
            // Arrange
            var matchFactory = new MatchFactory(Array.Empty<IMatchAdapter>());

            // Act
            var action = new Action(() => matchFactory.CreateInstance(ChallengeGame.LeagueOfLegends));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}
