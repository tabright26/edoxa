// Filename: ChallengeTest.cs
// Date Created: 2019-05-06
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Seedwork.Domain.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeTest
    {
        private static readonly FakeDefaultChallengeFactory FakeDefaultChallengeFactory = FakeDefaultChallengeFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Arrange
            var game = Game.LeagueOfLegends;
            var name = new ChallengeName(nameof(Challenge));
            var setup = new DefaultChallengeSetup();

            // Act
            var challenge = new Challenge(game, name, setup);

            // Assert
            challenge.Game.Should().Be(game);
            challenge.Name.Should().Be(name);
            challenge.Setup.Should().Be(setup);
            challenge.Scoring.Should().BeEmpty();

            //challenge.LiveData.Payout.Should().NotBeNull();

            //challenge.Payout.Should().NotBeEmpty();
            //challenge.Scoreboard.Should().BeEmpty();
            challenge.Participants.Should().BeEmpty();

            //challenge.LiveData.Entries.Should().Be(new Entries(challenge.Participants.Count, false));
            //challenge.LiveData.PayoutEntries.Should().Be(new PayoutEntries(challenge.LiveData.Entries, challenge.Setup.PayoutRatio));
            //challenge.LiveData.PrizePool.Should().Be(new PrizePool(challenge.LiveData.Entries, challenge.Setup.EntryFee, challenge.Setup.ServiceChargeRatio));
        }

        [TestMethod]
        public void Configure1_WhenTimelineStateIsDraft_ShouldBeConfigured()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallenge(ChallengeState.Draft);

            // Act
            challenge.Configure(
                FakeDefaultChallengeFactory.CreateScoringStrategy(),
                TimelinePublishedAt.Max,
                TimelineRegistrationPeriod.Default,
                TimelineExtensionPeriod.Default
            );

            // Assert
            challenge.Timeline.State.Should().Be(ChallengeState.Configured);
        }

        [TestMethod]
        public void Configure2_WhenTimelineStateIsDraft_ShouldBeConfigured()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallenge(ChallengeState.Draft);

            // Act
            challenge.Configure(FakeDefaultChallengeFactory.CreateScoringStrategy(), TimelinePublishedAt.Max);

            // Assert
            challenge.Timeline.State.Should().Be(ChallengeState.Configured);
        }

        [TestMethod]
        public void Publish1_WhenTimelineStateIsDraft_ShouldBeOpened()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallenge(ChallengeState.Draft);

            // Act
            challenge.Publish(FakeDefaultChallengeFactory.CreateScoringStrategy(), TimelineRegistrationPeriod.Default, TimelineExtensionPeriod.Default);

            // Assert
            challenge.Timeline.State.Should().Be(ChallengeState.Opened);
        }

        [TestMethod]
        public void Publish2_WhenTimelineStateIsDraft_ShouldBeOpened()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallenge(ChallengeState.Draft);

            // Act
            challenge.Publish(FakeDefaultChallengeFactory.CreateScoringStrategy());

            // Assert
            challenge.Timeline.State.Should().Be(ChallengeState.Opened);
        }

        [TestMethod]
        public void Close_WhenTimelineStateIsEnded_ShouldBeClosed()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallenge(ChallengeState.Ended);

            // Act
            challenge.Complete();

            // Assert
            challenge.Timeline.State.Should().Be(ChallengeState.Closed);
        }

        [TestMethod]
        public void Close_WithStateAsOpened_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallenge();

            // Act
            var action = new Action(() => challenge.Complete());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_IntoEmptyCollection_ShouldNotBeEmpty()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallenge();

            // Act
            challenge.RegisterParticipant(new UserId(), new LinkedAccount(Guid.NewGuid()));

            // Assert
            challenge.Participants.Should().NotBeEmpty();
        }

        [TestMethod]
        public void RegisterParticipant_WhoAlreadyExist_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var userId = new UserId();
            var challenge = FakeDefaultChallengeFactory.CreateChallengeWithParticipant(userId);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(userId, new LinkedAccount(Guid.NewGuid())));

            // Act => Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_WithEntriesFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallengeWithParticipants();

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new LinkedAccount(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_TimelineStateNotOpened_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallenge(ChallengeState.Draft);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new LinkedAccount(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantRegistered_ShouldNotThrowArgumentException()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallengeWithParticipant(new UserId());

            // Act
            var action = new Action(() =>
                challenge.SnapshotParticipantMatch(challenge.Participants.First().Id, FakeDefaultChallengeFactory.CreateMatchStats()));

            // Assert
            action.Should().NotThrow<ArgumentException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantNotRegistered_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeDefaultChallengeFactory.CreateChallenge(ChallengeState.Draft);

            // Act
            var action = new Action(() => challenge.SnapshotParticipantMatch(new ParticipantId(), FakeDefaultChallengeFactory.CreateMatchStats()));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}