// Filename: ChallengeTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Domain.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeTest
    {
        private ChallengeFaker _challengeFaker;

        [TestInitialize]
        public void TestInitialize()
        {
            _challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);
        }

        //[TestMethod]
        //public void RegisterParticipant_IntoEmptyCollection_ShouldNotBeEmpty()
        //{
        //    // Arrange
        //    var challenge = _challengeFaker.Generate();

        //    var participantCount = challenge.Participants.Count;

        //    // Act
        //    challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));

        //    // Assert
        //    challenge.Participants.Should().HaveCount(participantCount + 1);
        //}

        //[TestMethod]
        //public void RegisterParticipant_WhoAlreadyExist_ShouldThrowInvalidOperationException()
        //{
        //    // Arrange
        //    var challenge = _challengeFaker.Generate();

        //    // Act
        //    var action = new Action(
        //        () => challenge.Register(
        //            new Participant(challenge.Participants.First().UserId, new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider())
        //        )
        //    );

        //    // Act => Assert
        //    action.Should().Throw<InvalidOperationException>();
        //}

        //[TestMethod]
        //public void RegisterParticipant_WithEntriesFull_ShouldThrowInvalidOperationException()
        //{
        //    // Arrange
        //    var challenge = _challengeFaker.Generate();

        //    var participantCount = challenge.Setup.Entries - challenge.Participants.Count;

        //    for (var index = 0; index < participantCount; index++)
        //    {
        //        challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
        //    }

        //    // Act
        //    var action = new Action(
        //        () => challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()))
        //    );

        //    // Assert
        //    action.Should().Throw<InvalidOperationException>();
        //}
    }
}
