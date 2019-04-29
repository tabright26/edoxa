// Filename: ChallengeTest.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Seedwork.Domain.Common.Enums;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Domain.Tests.AggregateModels.ChallengeAggregate
{
    [TestClass]
    public sealed class ChallengeTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Arrange
            const Game game = Game.LeagueOfLegends;
            var name = new ChallengeName(nameof(Challenge));
            var setup = new DefaultChallengeSetup();

            // Act
            var challenge = ChallengeAggregateFactory.CreateChallenge(game, name, setup);

            // Assert
            challenge.Game.Should().Be(game);
            challenge.Name.Should().Be(name);
            challenge.Setup.Should().Be(setup);
            challenge.Scoring.Should().BeEmpty();
            challenge.LiveData.Payout.Should().NotBeNull();

            //challenge.Payout.Should().NotBeEmpty();
            challenge.Scoreboard.Should().BeEmpty();
            challenge.Participants.Should().BeEmpty();
            challenge.LiveData.Entries.Should().Be(new Entries(challenge.Participants.Count, false));
            challenge.LiveData.PayoutEntries.Should().Be(new PayoutEntries(challenge.LiveData.Entries, challenge.Setup.PayoutRatio));

            challenge.LiveData.PrizePool.Should()
                .Be(new PrizePool(challenge.LiveData.Entries, challenge.Setup.EntryFee, challenge.Setup.ServiceChargeRatio));
        }

        [TestMethod]
        public void Game_InvalidEnumArgument_ShouldThrowInvalidEnumArgumentException()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            var action = new Action(() => challenge.SetProperty(nameof(Challenge.Game), (Game) 1000));

            // Assert
            action.Should().Throw<InvalidEnumArgumentException>();
        }

        [DataTestMethod]
        [DataRow(Game.None)]
        [DataRow(Game.All)]
        public void Game_InvalidArgument_ShouldThrowArgumentException(Game game)
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            var action = new Action(() => challenge.SetProperty(nameof(Challenge.Game), game));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Configure1_WhenTimelineStateIsDraft_ShouldBeConfigured()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            challenge.Configure(
                ChallengeAggregateFactory.CreateChallengeScoringStrategy(),
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
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            challenge.Configure(ChallengeAggregateFactory.CreateChallengeScoringStrategy(), TimelinePublishedAt.Max);

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState1.Configured);
        }

        [TestMethod]
        public void Publish1_WhenTimelineStateIsDraft_ShouldBeOpened()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            challenge.Publish(ChallengeAggregateFactory.CreateChallengeScoringStrategy(), TimelineRegistrationPeriod.Default, TimelineExtensionPeriod.Default);

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState1.Opened);
        }

        [TestMethod]
        public void Publish2_WhenTimelineStateIsDraft_ShouldBeOpened()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            challenge.Publish(ChallengeAggregateFactory.CreateChallengeScoringStrategy());

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState1.Opened);
        }

        [TestMethod]
        public void Close_WhenTimelineStateIsEnded_ShouldBeClosed()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Ended);

            // Act
            challenge.Close();

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState1.Closed);
        }

        [TestMethod]
        public void Close_WithStateAsOpened_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge();

            // Act
            var action = new Action(() => challenge.Close());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_IntoEmptyCollection_ShouldNotBeEmpty()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge();

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
            var challenge = ChallengeAggregateFactory.CreateChallengeWithParticipant(userId);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(userId, new LinkedAccount(Guid.NewGuid())));

            // Act => Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_WithEntriesFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallengeWithParticipants();

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new LinkedAccount(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_TimelineStateNotOpened_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new LinkedAccount(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantRegistered_ShouldNotThrowArgumentException()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallengeWithParticipant(new UserId());

            // Act
            var action = new Action(() =>
                challenge.SnapshotParticipantMatch(challenge.Participants.First().Id, ChallengeAggregateFactory.CreateChallengeStats()));

            // Assert
            action.Should().NotThrow<ArgumentException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantNotRegistered_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = ChallengeAggregateFactory.CreateChallenge(ChallengeState1.Draft);

            // Act
            var action = new Action(() => challenge.SnapshotParticipantMatch(new ParticipantId(), ChallengeAggregateFactory.CreateChallengeStats()));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}