// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Entities.AggregateModels;
using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.Infrastructure.Repositories;
using eDoxa.Seedwork.Enumerations;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Infrastructure.Tests.Repositories
{
    [TestClass]
    public sealed class ChallengeRepositoryTest
    {
        private static readonly FakeRandomChallengeFactory FakeRandomChallengeFactory = FakeRandomChallengeFactory.Instance;

        [TestMethod]
        public async Task Create_Challenge_ShouldNotBeEmpty()
        {
            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new ChallengeRepository(context);

                    var challenge = FakeRandomChallengeFactory.CreateRandomChallenges().First();

                    // Act
                    repository.Create(challenge);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Assert
                    context.Challenges.Should().NotBeEmpty();
                }
            }
        }

        [TestMethod]
        public async Task Create_Challenges_ShouldNotBeEmpty()
        {
            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new ChallengeRepository(context);

                    var challenges = FakeRandomChallengeFactory.CreateRandomChallenges();

                    // Act
                    repository.Create(challenges);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Assert
                    context.Challenges.Should().NotBeEmpty();
                }
            }
        }

        [TestMethod]
        public async Task FindChallengesAsync_Persistent_ShouldBeLoaded()
        {
            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new ChallengeRepository(context);

                    var challenges = FakeRandomChallengeFactory.CreateRandomChallenges();

                    repository.Create(challenges);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new ChallengeRepository(context);

                    // Act
                    var challenges =
                        await repository.FindChallengesAsync(Game.All, ChallengeType.All, ChallengeState1.All);

                    // Assert
                    ChallengeRepositoryAssert.IsLoaded(challenges);
                }
            }
        }

        [TestMethod]
        public async Task FindChallengesAsync_NotPersistent_ShouldBeEmpty()
        {
            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new ChallengeRepository(context);

                    // Act
                    var challenges =
                        await repository.FindChallengesAsync(Game.All, ChallengeType.All, ChallengeState1.All);

                    // Assert
                    challenges.Should().BeEmpty();
                }
            }
        }

        [DataRow(Game.All, ChallengeType.All, ChallengeState1.None)]
        [DataRow(Game.All, ChallengeType.None, ChallengeState1.All)]
        [DataRow(Game.None, ChallengeType.All, ChallengeState1.All)]
        [DataTestMethod]
        public async Task FindChallengesAsync_ByNoneFlags_ShouldBeEmpty(
            Game game,
            ChallengeType type,
            ChallengeState1 state)
        {
            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context);

                    var challenges = FakeRandomChallengeFactory.CreateRandomChallenges();

                    repository.Create(challenges);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new ChallengeRepository(context);

                    // Act
                    var challenges = await repository.FindChallengesAsync(game, type, state);

                    // Assert
                    challenges.Should().BeEmpty();
                }
            }
        }

        [DataRow(ChallengeState1.Draft)]
        [DataRow(ChallengeState1.Configured)]
        [DataRow(ChallengeState1.Opened)]
        [DataRow(ChallengeState1.InProgress)]
        [DataRow(ChallengeState1.Ended)]
        [DataRow(ChallengeState1.Closed)]
        [DataTestMethod]
        public async Task FindChallengesAsync_ByState_ShouldHaveCountOfFive(ChallengeState1 state)
        {
            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context);

                    var challenges = FakeRandomChallengeFactory.CreateRandomChallenges(state);

                    repository.Create(challenges);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new ChallengeRepository(context);

                    // Act
                    var challenges = await repository.FindChallengesAsync(Game.All, ChallengeType.All, state);

                    // Assert
                    challenges.Should().HaveCount(FakeRandomChallengeFactory.DefaultRandomChallengeCount);
                }
            }
        }

        [TestMethod]
        public async Task FindChallengeAsync_Persistent_ShouldBeLoaded()
        {
            var challenge = FakeRandomChallengeFactory.CreateRandomChallenge();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context);

                    repository.Create(challenge);

                    await repository.UnitOfWork.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new ChallengeRepository(context);

                    // Act
                    challenge = await repository.FindChallengeAsync(challenge.Id);

                    // Assert
                    ChallengeRepositoryAssert.IsLoaded(challenge);
                }
            }
        }

        [TestMethod]
        public async Task FindChallengeAsync_NotPersistent_ShouldBeNull()
        {
            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Arrange
                    var repository = new ChallengeRepository(context);

                    // Act
                    var challenge = await repository.FindChallengeAsync(new ChallengeId());

                    // Assert
                    challenge.Should().BeNull();
                }
            }
        }

        private static class ChallengeRepositoryAssert
        {
            internal static void IsLoaded(IEnumerable<Challenge> challenges)
            {
                foreach (var challenge in challenges)
                {
                    IsLoaded(challenge);
                }
            }

            internal static void IsLoaded(Challenge challenge)
            {
                challenge.Should().NotBeNull();
                challenge.Id.ToGuid().Should().NotBeEmpty();
                challenge.Game.Should().NotBe(Game.All);
                challenge.Game.Should().NotBe(Game.None);
                challenge.Name.ToString().Should().NotBeNullOrWhiteSpace();
                challenge.Setup.Should().NotBeNull();
                challenge.Timeline.Should().NotBeNull();
                //challenge.LiveData.Should().NotBeNull();
                challenge.Scoring.Should().NotBeNull();
                //challenge.Payout.Should().NotBeNull();
                //challenge.Scoreboard.Should().NotBeNull();
                challenge.Participants.Should().NotBeNullOrEmpty();

                foreach (var participant in challenge.Participants)
                {
                    participant.Id.ToGuid().Should().NotBeEmpty();
                    participant.Timestamp.Should().BeBefore(DateTime.UtcNow);
                    participant.LinkedAccount.ToString().Should().NotBeNullOrWhiteSpace();
                    participant.UserId.ToGuid().Should().NotBeEmpty();
                    participant.Challenge.Should().NotBeNull();
                    participant.Matches.Should().NotBeNullOrEmpty();

                    foreach (var match in participant.Matches)
                    {
                        match.Id.ToGuid().Should().NotBeEmpty();
                        match.Timestamp.Should().BeBefore(DateTime.UtcNow);
                        match.LinkedMatch.ToString().Should().NotBeNullOrWhiteSpace();
                        match.TotalScore.Should().NotBeNull();
                        match.Participant.Should().NotBeNull();
                        match.Stats.Should().NotBeNullOrEmpty();

                        foreach (var stat in match.Stats)
                        {
                            stat.Id.ToGuid().Should().NotBeEmpty();
                            stat.MatchId.ToGuid().Should().NotBeEmpty();
                            stat.Name.Should().NotBeNull();
                            stat.Score.Should().NotBeNull();
                        }
                    }
                }
            }
        }
    }
}