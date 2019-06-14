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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Seedwork.Common.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ParticipantAggregate
{
    [TestClass]
    public sealed class ParticipantTest
    {
        public static IEnumerable<object[]> Data => Game.GetAll().Select(game => new object[] {game});

        [DataTestMethod]
        [DynamicData(nameof(Data))]
        public void SnapshotMatch_Matches_ShouldNotBeEmpty(Game game)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(game);
            var participant = challengeFaker.Generate().Participants.First();

            var faker = new Faker();
            var matchReference = faker.MatchReference(game);
            var stats = faker.MatchStats(game);

            // Act
            participant.SnapshotMatch(matchReference, stats);

            // Assert
            participant.Matches.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Matches_ShouldHaveCountOf()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(Game.LeagueOfLegends, ChallengeState.InProgress);
            var challenge = challengeFaker.Generate();
            
            // Act
            var matches = challenge.Participants.First().Matches;

            // Assert
            matches.Should().NotBeEmpty();
        }

        //[DataRow(1, 1)]
        //[DataRow(3, 3)]
        //[DataRow(5, 5)]
        //[DataTestMethod]
        //public void AverageScore_MatchCountGreaterThanOrEqualToBestOf_ShouldNotBeNull(int matchCount, int bestOf)
        //{
        //    // Arrange
        //    var participant = FakeChallengeFactory.CreateParticipantMatches(matchCount, new BestOf(bestOf));

        //    // Act
        //    var score = participant.AverageScore;

        //    // Assert
        //    score.Should().NotBeNull();
        //}
    }
}
