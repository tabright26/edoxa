// Filename: ParticipantFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class ParticipantFakerTest
    {
        [TestMethod]
        public void FakeParticipant_ShouldNotThrow()
        {
            // Arrange
            var participantFaker = new ParticipantFaker();

            // Act
            var participant = participantFaker.FakeParticipant(Game.LeagueOfLegends, ChallengeState.Closed, BestOf.Five);

            participant.DumbAsJson(true);

            // Assert
            var matchReferences = participant.Matches.Select(match => match.Reference).ToList();

            matchReferences.Should().BeEquivalentTo(matchReferences.Distinct());
        }
    }
}
