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

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Arena.Challenges.Services.Factories;
using eDoxa.Arena.Challenges.Tests.Factories;
using eDoxa.Seedwork.Domain.Enumerations;

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
            var challenge = CreateChallengeType5();

            var scoreboard = challenge.Scoreboard;

            var participantPrizes = challenge.Payout.GetParticipantPrizes(challenge.Scoreboard);
        }

        private static Challenge MockChallenge()
        {
            var payout = new Payout(
                new Buckets
                {
                    new Bucket(new Prize(10M), 1),
                    new Bucket(new Prize(7.5M), 1),
                    new Bucket(new Prize(2.5M), 1)
                }
            );

            var challenge = new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Type1"),
                new ChallengeSetup(
                    new BestOf(1),
                    new Entries(10),
                    new EntryFee(2.5M),
                    new PayoutRatio(0.3F),
                    new ServiceChargeRatio(0.2F)
                ),
                new ChallengeDuration(),
                payout,
                ScoringFactory.Instance.CreateScoringStrategy(Game.LeagueOfLegends)
            );

            return AddParticipants(challenge);
        }

        private static Challenge AddParticipants(Challenge challenge)
        {
            for (var index = 0; index < challenge.Setup.Entries; index++)
            {
                var userId = new UserId();

                challenge.RegisterParticipant(userId, new ParticipantExternalAccount(Guid.NewGuid()));

                var participantId = challenge.Participants.Single(participant => participant.UserId == userId).Id;

                var random = new Random();

                for (var i = 0; i < random.Next(2, challenge.Setup.BestOf + 2); i++)
                {
                    challenge.SnapshotParticipantMatch(participantId, CreateMatchStats());
                }
            }

            return challenge;
        }

        private static IMatchStats CreateMatchStats(MatchExternalId matchExternalId = null)
        {
            matchExternalId = matchExternalId ?? new MatchExternalId(2233345251);

            var random = new Random();

            return new MatchStats(
                matchExternalId,
                new
                {
                    Kills = random.Next(0, 40 + 1),
                    Deaths = random.Next(0, 15 + 1),
                    Assists = random.Next(0, 50 + 1),
                    TotalDamageDealtToChampions = random.Next(10000, 500000 + 1),
                    TotalHeal = random.Next(10000, 350000 + 1)
                }
            );
        }

        public static Challenge CreateChallengeType5()
        {
            var payout = new Payout(
                new Buckets
                {
                    new Bucket(new Prize(25M), 1),
                    new Bucket(new Prize(20M), 1),
                    new Bucket(new Prize(12.5M), 2),
                    new Bucket(new Prize(10M), 3),
                    new Bucket(new Prize(7M), 5),
                    new Bucket(new Prize(5M), 13)
                }
            );

            var setup = new ChallengeSetup(
                new BestOf(3),
                new Entries(50),
                new EntryFee(5M),
                new PayoutRatio(0.5F),
                new ServiceChargeRatio(0.2F)
            );

            var challenge = new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Type5"),
                setup,
                new ChallengeDuration(),
                payout,
                ScoringFactory.Instance.CreateScoringStrategy(Game.LeagueOfLegends)
            );

            return AddParticipants(challenge);
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
            challenge.RegisterParticipant(new UserId(), new ParticipantExternalAccount(Guid.NewGuid()));

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
            var action = new Action(() => challenge.RegisterParticipant(userId, new ParticipantExternalAccount(Guid.NewGuid())));

            // Act => Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [TestMethod]
        public void RegisterParticipant_WithEntriesFull_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var challenge = FakeChallengeFactory.CreateChallengeWithParticipants();

            // Act
            var action = new Action(() => challenge.RegisterParticipant(new UserId(), new ParticipantExternalAccount(Guid.NewGuid())));

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
