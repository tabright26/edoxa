// Filename: StatScoreResolverTest.cs
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
    public sealed class StatScoreResolverTest
    {
        private static IEnumerable<object[]> GameDataSets => ChallengeGame.GetEnumerations().Select(game => new object[] {game}).ToList();

        [DataTestMethod]
        [DynamicData(nameof(GameDataSets))]
        public void Resolve_StatViewModel_ShouldBeStatScore(ChallengeGame game)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(game, ChallengeState.Closed);
            var fakeChallenge = challengeFaker.Generate();

            // Act
            var challengeViewModel = fakeChallenge.ToViewModel();

            // Assert
            foreach (var match in fakeChallenge.Participants.SelectMany(participant => participant.Matches))
            {
                var matchViewModel = challengeViewModel.Participants.SelectMany(participantViewModel => participantViewModel.Matches)
                    .First(viewModel => viewModel.Id == match.Id);

                foreach (var stat in match.Stats)
                {
                    var statViewModel = matchViewModel.Stats.First(viewModel => viewModel.Name == stat.Name);

                    statViewModel.Score.Should().Be(stat.Score);
                }
            }
        }
    }
}
