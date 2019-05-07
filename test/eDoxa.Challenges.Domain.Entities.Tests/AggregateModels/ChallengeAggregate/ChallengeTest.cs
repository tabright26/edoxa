// Filename: ChallengeTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.Entities.Default;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Seedwork.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Entities.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeTest
    {
        private static readonly FakeChallengeFactory FakeChallengeFactory = FakeChallengeFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Arrange
            const Game game = Game.LeagueOfLegends;
            var name = new ChallengeName(nameof(Challenge));
            var setup = new DefaultChallengeSetup();

            // Act
            var challenge = FakeChallengeFactory.CreateChallenge(game, name, setup);

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
            var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            challenge.Configure(
                FakeChallengeFactory.CreateScoringStrategy(),
                TimelinePublishedAt.Max,
                TimelineRegistrationPeriod.Default,
                TimelineExtensionPeriod.Default
            );

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState1.Configured);
        }

        [TestMethod]
        public void Configure2_WhenTimelineStateIsDraft_ShouldBeConfigured()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            challenge.Configure(FakeChallengeFactory.CreateScoringStrategy(), TimelinePublishedAt.Max);

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState1.Configured);
        }

        [TestMethod]
        public void Publish1_WhenTimelineStateIsDraft_ShouldBeOpened()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            challenge.Publish(FakeChallengeFactory.CreateScoringStrategy(), TimelineRegistrationPeriod.Default, TimelineExtensionPeriod.Default);

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState1.Opened);
        }

        [TestMethod]
        public void Publish2_WhenTimelineStateIsDraft_ShouldBeOpened()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            challenge.Publish(FakeChallengeFactory.CreateScoringStrategy());

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState1.Opened);
        }

        [TestMethod]
        public void Close_WhenTimelineStateIsEnded_ShouldBeClosed()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState1.Ended);

            // Act
            challenge.Complete();

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState1.Closed);
        }

        [TestMethod]
        public void Close_WithStateAsOpened_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge();

            // Act
            var action = new Action(() => challenge.Complete());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_IntoEmptyCollection_ShouldNotBeEmpty()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge();

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
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipant(userId);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(userId, new LinkedAccount(Guid.NewGuid())));

            // Act => Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_WithEntriesFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipants();

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new LinkedAccount(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_TimelineStateNotOpened_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new LinkedAccount(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantRegistered_ShouldNotThrowArgumentException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipant(new UserId());

            // Act
            var action = new Action(() =>
                challenge.SnapshotParticipantMatch(challenge.Participants.First().Id, FakeChallengeFactory.CreateMatchStats()));

            // Assert
            action.Should().NotThrow<ArgumentException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantNotRegistered_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            var action = new Action(() => challenge.SnapshotParticipantMatch(new ParticipantId(), FakeChallengeFactory.CreateMatchStats()));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}