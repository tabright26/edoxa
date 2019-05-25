// Filename: ChallengeTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Tests.Factories;
using eDoxa.Arena.Domain;
using eDoxa.Seedwork.Domain.Entities;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;

        [TestMethod]
        public void M()
        {
            //var challenge = ChallengeFactory.Create(Game.LeagueOfLegends, ChallengeType.Type5);

            //var scoreboard = challenge.Scoreboard;

            //var participantPrizes = challenge.Payout.GetParticipantPrizes(challenge.Scoreboard);
        }

        ////[TestMethod]
        ////public void Constructor_Initialize_ShouldNotThrowException()
        ////{
        ////    Arrange
        ////   var game = Game.LeagueOfLegends;
        ////    var name = new ChallengeName(nameof(Challenge));
        ////    var setup = new FakeChallengeSetup();

        ////    Act
        ////   var challenge = new Challenge(game, name, setup, ScoringFactory.Instance.CreateScoringStrategy(Game.LeagueOfLegends));

        ////    Assert
        ////    challenge.Game.Should().Be(game);
        ////    challenge.Name.Should().Be(name);
        ////    challenge.Setup.Should().Be(setup);
        ////    challenge.Scoring.Should().NotBeEmpty();

        ////    challenge.LiveData.Payout.Should().NotBeNull();

        ////    challenge.Payout.Should().NotBeEmpty();
        ////    challenge.Scoreboard.Should().BeEmpty();
        ////    challenge.Participants.Should().BeEmpty();

        ////    challenge.LiveData.Entries.Should().Be(new Entries(challenge.Participants.Count, false));
        ////    challenge.LiveData.PayoutEntries.Should().Be(new PayoutEntries(challenge.LiveData.Entries, challenge.Setup.PayoutRatio));
        ////    challenge.LiveData.PrizePool.Should().Be(new PrizePool(challenge.LiveData.Entries, challenge.Setup.EntryFee, challenge.Setup.ServiceChargeRatio));
        ////}

        //[TestMethod]
        //public void Configure1_WhenTimelineStateIsDraft_ShouldBeConfigured()
        //{
        //    // Arrange
        //    var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState.Draft);

        //    // Act
        //    challenge.Configure(
        //        FakeChallengeFactory.CreateScoringStrategy(),
        //        TimelinePublishedAt.Max,
        //        TimelineRegistrationPeriod.Default,
        //        TimelineExtensionPeriod.Default
        //    );

        //    // Assert
        //    challenge.Timeline.State.Should().Be(ChallengeState.Configured);
        //}

        //[TestMethod]
        //public void Configure2_WhenTimelineStateIsDraft_ShouldBeConfigured()
        //{
        //    // Arrange
        //    var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState.Draft);

        //    // Act
        //    challenge.Configure(FakeChallengeFactory.CreateScoringStrategy(), TimelinePublishedAt.Max);

        //    // Assert
        //    challenge.Timeline.State.Should().Be(ChallengeState.Configured);
        //}

        //[TestMethod]
        //public void Publish1_WhenTimelineStateIsDraft_ShouldBeOpened()
        //{
        //    // Arrange
        //    var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState.Draft);

        //    // Act
        //    challenge.Publish(FakeChallengeFactory.CreateScoringStrategy(), TimelineRegistrationPeriod.Default, TimelineExtensionPeriod.Default);

        //    // Assert
        //    challenge.Timeline.State.Should().Be(ChallengeState.Opened);
        //}

        //[TestMethod]
        //public void Publish2_WhenTimelineStateIsDraft_ShouldBeOpened()
        //{
        //    // Arrange
        //    var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState.Draft);

        //    // Act
        //    challenge.Publish(FakeChallengeFactory.CreateScoringStrategy());

        //    // Assert
        //    challenge.Timeline.State.Should().Be(ChallengeState.Opened);
        //}

        //[TestMethod]
        //public void Close_WhenTimelineStateIsEnded_ShouldBeClosed()
        //{
        //    // Arrange
        //    var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState.Ended);

        //    // Act
        //    challenge.Complete();

        //    // Assert
        //    challenge.Timeline.State.Should().Be(ChallengeState.Closed);
        //}

        //[TestMethod]
        //public void Close_WithStateAsOpened_ShouldThrowInvalidOperationException()
        //{
        //    // Arrange
        //    var challenge = FakeChallengeFactory.CreateChallenge();

        //    // Act
        //    var action = new Action(() => challenge.Complete());

        //    // Assert
        //    action.Should().Throw<InvalidOperationException>();
        //}

        [TestMethod]
        public void RegisterParticipant_IntoEmptyCollection_ShouldNotBeEmpty()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge();

            // Act
            challenge.RegisterParticipant(new UserId(), new ExternalAccount(Guid.NewGuid()));

            // Assert
            challenge.Participants.Should().NotBeEmpty();
        }

        [TestMethod]
        public void RegisterParticipant_WhoAlreadyExist_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = new UserId();
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipant(userId);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(userId, new ExternalAccount(Guid.NewGuid())));

            // Act => Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_WithEntriesFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipants();

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new ExternalAccount(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        //[TestMethod]
        //public void RegisterParticipant_TimelineStateNotOpened_ShouldThrowInvalidOperationException()
        //{
        //    // Arrange
        //    var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState.Draft);

        //    // Act
        //    var action = new Action(() => challenge.RegisterParticipant(new UserId(), new ParticipantExternalAccount(Guid.NewGuid())));

        //    // Assert
        //    action.Should().Throw<InvalidOperationException>();
        //}

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantRegistered_ShouldNotThrowArgumentException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipant(new UserId());

            // Act
            var action = new Action(() => challenge.SnapshotParticipantMatch(challenge.Participants.First().Id, FakeChallengeFactory.CreateMatchStats()));

            // Assert
            action.Should().NotThrow<ArgumentException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantNotRegistered_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState.Draft);

            // Act
            var action = new Action(() => challenge.SnapshotParticipantMatch(new ParticipantId(), FakeChallengeFactory.CreateMatchStats()));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
