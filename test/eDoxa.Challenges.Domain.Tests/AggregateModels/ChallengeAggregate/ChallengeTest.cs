// Filename: ChallengeTest.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel;
using System.Linq;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Helpers;
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
        private static readonly ChallengeAggregateFactory _factory = ChallengeAggregateFactory.Instance;

        [TestMethod]
        public void Constructor_Initialize_ShouldNotThrowException()
        {
            // Arrange
            var helper = new ChallengeHelper();
            const Game game = Game.LeagueOfLegends;
            var name = new ChallengeName(nameof(Challenge));
            var settings = new ChallengeSettings();

            // Act
            var challenge = _factory.CreateChallenge(game, name, settings);

            // Assert
            challenge.Game.Should().Be(game);
            challenge.Name.Should().Be(name);
            challenge.Settings.Should().Be(settings);
            challenge.Scoring.Should().BeNull();
            challenge.LiveData.PrizeBreakdown.Should().BeEmpty();
            challenge.PrizeBreakdown.Should().NotBeEmpty();
            challenge.Scoreboard.Should().BeEmpty();
            challenge.Participants.Should().BeEmpty();
            challenge.LiveData.Entries.Should().Be(challenge.Participants.Count);
            challenge.LiveData.PayoutEntries.Should().Be(helper.PayoutEntries(challenge.LiveData.Entries, challenge.Settings.PayoutRatio));
            challenge.LiveData.PrizePool.Should().Be(helper.PrizePool(challenge.LiveData.Entries, challenge.Settings.EntryFee, challenge.Settings.ServiceChargeRatio));
        }

        [TestMethod]
        public void Game_InvalidEnumArgument_ShouldThrowInvalidEnumArgumentException()
        {
            // Arrange
            var challenge = _factory.CreateChallenge(ChallengeState.Draft);

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
            var challenge = _factory.CreateChallenge(ChallengeState.Draft);

            // Act
            var action = new Action(() => challenge.SetProperty(nameof(Challenge.Game), game));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void Configure1_WhenTimelineStateIsDraft_ShouldBeConfigured()
        {
            // Arrange
            var challenge = _factory.CreateChallenge(ChallengeState.Draft);

            // Act
            challenge.Configure(
                _factory.CreateChallengeScoringStrategy(),
                ChallengeTimeline.MaxPublishedAt,
                ChallengeTimeline.DefaultRegistrationPeriod,
                ChallengeTimeline.DefaultExtensionPeriod
            );

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState.Configured);
        }

        [TestMethod]
        public void Configure2_WhenTimelineStateIsDraft_ShouldBeConfigured()
        {
            // Arrange
            var challenge = _factory.CreateChallenge(ChallengeState.Draft);

            // Act
            challenge.Configure(_factory.CreateChallengeScoringStrategy(), ChallengeTimeline.MaxPublishedAt);

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState.Configured);
        }

        [TestMethod]
        public void Publish1_WhenTimelineStateIsDraft_ShouldBeOpened()
        {
            // Arrange
            var challenge = _factory.CreateChallenge(ChallengeState.Draft);

            // Act
            challenge.Publish(_factory.CreateChallengeScoringStrategy(), ChallengeTimeline.DefaultRegistrationPeriod, ChallengeTimeline.DefaultExtensionPeriod);

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState.Opened);
        }

        [TestMethod]
        public void Publish2_WhenTimelineStateIsDraft_ShouldBeOpened()
        {
            // Arrange
            var challenge = _factory.CreateChallenge(ChallengeState.Draft);

            // Act
            challenge.Publish(_factory.CreateChallengeScoringStrategy());

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState.Opened);
        }

        [TestMethod]
        public void Close_WhenTimelineStateIsEnded_ShouldBeClosed()
        {
            // Arrange
            var challenge = _factory.CreateChallenge(ChallengeState.Ended);

            // Act
            challenge.Close();

            // Assert
            challenge.Timeline.State.Should().HaveFlag(ChallengeState.Closed);
        }

        [TestMethod]
        public void Close_WithStateAsOpened_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = _factory.CreateChallenge();

            // Act
            var action = new Action(() => challenge.Close());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_IntoEmptyCollection_ShouldNotBeEmpty()
        {
            // Arrange
            var challenge = _factory.CreateChallenge();

            // Act
            challenge.RegisterParticipant(new UserId(), LinkedAccount.FromGuid(Guid.NewGuid()));

            // Assert
            challenge.Participants.Should().NotBeEmpty();
        }

        [TestMethod]
        public void RegisterParticipant_WhoAlreadyExist_ShouldThrowArgumentException()
        {
            // Arrange
            var userId = new UserId();
            var challenge = _factory.CreateChallengeWithParticipant(userId);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(userId, LinkedAccount.FromGuid(Guid.NewGuid())));

            // Act => Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
        public void RegisterParticipant_WithEntriesFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = _factory.CreateChallengeWithParticipants();

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), LinkedAccount.FromGuid(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_TimelineStateNotOpened_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = _factory.CreateChallenge(ChallengeState.Draft);

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), LinkedAccount.FromGuid(Guid.NewGuid())));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantRegistered_ShouldNotThrowArgumentException()
        {
            // Arrange
            var challenge = _factory.CreateChallengeWithParticipant(new UserId());

            // Act
            var action = new Action(() => challenge.SnapshotParticipantMatch(challenge.Participants.First().Id, _factory.CreateChallengeStats()));

            // Assert
            action.Should().NotThrow<ArgumentException>();
        }

        [TestMethod]
        public void SnapshotParticipantMatch_ParticipantNotRegistered_ShouldThrowArgumentException()
        {
            // Arrange
            var challenge = _factory.CreateChallenge(ChallengeState.Draft);

            // Act
            var action = new Action(() => challenge.SnapshotParticipantMatch(new ParticipantId(), _factory.CreateChallengeStats()));

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}