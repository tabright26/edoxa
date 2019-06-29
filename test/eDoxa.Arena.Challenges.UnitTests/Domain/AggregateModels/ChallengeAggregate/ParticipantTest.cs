// Filename: ParticipantTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ParticipantTest
    {
        private static readonly Faker Faker = new Faker();

        private static IEnumerable<object[]> ChallengeGames => ChallengeGame.GetEnumerations().Select(game => new object[] {game});

        [DataTestMethod]
        [DynamicData(nameof(ChallengeGames))]
        public void SnapshotMatch_ParticipantMatches_ShouldNotBeEmpty(ChallengeGame game)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(game);
            var challenge = challengeFaker.Generate();
            var participant = challenge.Participants.First();
            var gameId = Faker.Match().GameId(game);
            var stats = Faker.Match().Stats(game);

            // Act
            var match = new Match(gameId, new UtcNowDateTimeProvider());
            match.Snapshot(stats, challenge.Scoring);
            participant.Snapshot(match);

            // Assert
            participant.Matches.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Matches_ShouldHaveCountOf()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(ChallengeGame.LeagueOfLegends, ChallengeState.InProgress);
            var challenge = challengeFaker.Generate();

            // Act
            var matches = challenge.Participants.First().Matches;

            // Assert
            matches.Should().NotBeEmpty();
        }
    }
}
