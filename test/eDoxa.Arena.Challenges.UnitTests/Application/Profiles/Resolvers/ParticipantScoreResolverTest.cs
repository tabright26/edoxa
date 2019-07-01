// Filename: ParticipantScoreResolverTest.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Profiles.Resolvers
{
    [TestClass]
    public sealed class ParticipantScoreResolverTest
    {
        private static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game}).ToList();

        [DataTestMethod]
        [DynamicData(nameof(GameDataSets))]
        public void Resolve_ParticipantViewModel_ShouldBeParticipantScore(ChallengeGame game)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(game, ChallengeState.Closed);
            var challenge = challengeFaker.Generate();

            // Act
            var challengeViewModel = challenge.ToViewModel();

            // Assert
            foreach (var participant in challenge.Participants)
            {
                var participantViewModel = challengeViewModel.Participants.Single(viewModel => viewModel.Id == participant.Id);

                participantViewModel.AverageScore.Should().Be(participant.AverageScore(challenge.Setup.BestOf));
            }
        }
    }
}
