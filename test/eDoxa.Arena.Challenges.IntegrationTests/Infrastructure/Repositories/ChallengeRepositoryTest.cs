// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Infrastructure.Repositories
{
    [TestClass]
    public sealed class ChallengeRepositoryTest
    {
        private static readonly IMapper Mapper = MapperBuilder.CreateMapper();

        [TestMethod]
        public async Task Create_Challenge_ShouldNotBeNull()
        {
            //var challengeFaker = new ChallengeFaker();

            var challenge = CreateChallenge();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    repository.Create(challenge);

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenges = await repository.FindChallengesAsync();

                    challenges.Should().NotBeNull();

                    //challenge.Should().NotBeNull();

                    //challenge.ShouldBeValidObjectState();

                    //var challengeAsNoTracking = await repository.FindChallengeAsync(challenge.Id);

                    //challengeAsNoTracking.Should().NotBeNull();

                    //challengeAsNoTracking.ShouldBeValidObjectState();

                    //challengeAsNoTracking.Should().BeEquivalentTo(challenge);
                }
            }
        }

        //[DataRow(2)]
        //[DataRow(5)]
        //[DataRow(10)]
        //[DataTestMethod]
        //public async Task Create_Challenges_ShouldHaveCount(int count)
        //{
        //    var challengeFaker = new ChallengeFaker();

        //    using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
        //    {
        //        using (var context = factory.CreateContext())
        //        {
        //            var repository = new ChallengeRepository(context, Mapper);

        //            repository.Create(challengeFaker.Generate(count));

        //            await repository.UnitOfWork.CommitAsync();
        //        }

        //        using (var context = factory.CreateContext())
        //        {
        //            var repository = new ChallengeRepository(context, Mapper);

        //            var challenges = await repository.FindChallengesAsync();

        //            challenges.Should().HaveCount(count);

        //            challenges.ShouldBeValidObjectState();

        //            //var challengesAsNoTracking = await repository.FindChallengesAsNoTrackingAsync();

        //            //challengesAsNoTracking.Should().HaveCount(count);

        //            //challengesAsNoTracking.ShouldBeValidObjectState();

        //            //challengesAsNoTracking.Should().BeEquivalentTo(challenges);
        //        }
        //    }
        //}

        [TestMethod]
        public async Task Update_Challenge_ShouldNotBeNull()
        {
            var faker = new Faker();

            var challenge = new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Challenge"),
                new ChallengeSetup(BestOf.Three, PayoutEntries.One, MoneyEntryFee.Five),
                ChallengeDuration.TwoDays,
                new UtcNowDateTimeProvider()
            );

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    repository.Create(challenge);

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challengeFromRepository = await repository.FindChallengeAsync(challenge.Id);

                    challengeFromRepository.Should().Be(challenge);

                    challengeFromRepository.State.Should().Be(ChallengeState.Inscription);
                }

                var participant1 = new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider());

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challengeFromRepository = await repository.FindChallengeAsync(challenge.Id);

                    challengeFromRepository.Register(participant1);

                    await repository.CommitAsync();

                    challengeFromRepository.State.Should().Be(ChallengeState.Inscription);
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challengeFromRepository = await repository.FindChallengeAsync(challenge.Id);

                    challengeFromRepository.Participants.Should().Contain(participant1);

                    challengeFromRepository.State.Should().Be(ChallengeState.Inscription);
                }

                var participant2 = new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider());

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challengeFromRepository = await repository.FindChallengeAsync(challenge.Id);

                    challengeFromRepository.Register(participant2);

                    await repository.CommitAsync();

                    challengeFromRepository.State.Should().Be(ChallengeState.InProgress);
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challengeFromRepository = await repository.FindChallengeAsync(challenge.Id);

                    challengeFromRepository.Participants.Should().BeEquivalentTo(participant1, participant2);

                    challengeFromRepository.State.Should().Be(ChallengeState.InProgress);
                }

                var match1 = new Match(new GameMatchId(Guid.NewGuid()), new UtcNowDateTimeProvider());

                match1.SnapshotStats(challenge.Scoring, faker.MatchStats(Game.LeagueOfLegends));

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challengeFromRepository = await repository.FindChallengeAsync(challenge.Id);

                    var par = challengeFromRepository.Participants.Single(p => p == participant1);

                    par.Synchronize(match1);

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challengeFromRepository = await repository.FindChallengeAsync(challenge.Id);

                    var par = challengeFromRepository.Participants.Single(p => p == participant1);

                    par.Matches.Should().Contain(match1);

                    par.SynchronizedAt.Should().BeNull();

                    par.AverageScore(challenge.Setup.BestOf).Should().BeNull();

                    challengeFromRepository.State.Should().Be(ChallengeState.InProgress);
                }

                var match2 = new Match(new GameMatchId(Guid.NewGuid()), new UtcNowDateTimeProvider());

                match2.SnapshotStats(challenge.Scoring, faker.MatchStats(Game.LeagueOfLegends));

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challengeFromRepository = await repository.FindChallengeAsync(challenge.Id);

                    var par = challengeFromRepository.Participants.Single(p => p == participant1);

                    par.Synchronize(match2);

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challengeFromRepository = await repository.FindChallengeAsync(challenge.Id);

                    var par = challengeFromRepository.Participants.Single(p => p == participant1);

                    par.Matches.Should().BeEquivalentTo(match1, match2);

                    par.SynchronizedAt.Should().BeNull();

                    par.AverageScore(challenge.Setup.BestOf).Should().BeNull();

                    challengeFromRepository.State.Should().Be(ChallengeState.InProgress);
                }
            }
        }

        private static Challenge CreateChallenge()
        {
            var faker = new Faker();

            var challenge = new Challenge(
                Game.LeagueOfLegends,
                new ChallengeName("Challenge"),
                new ChallengeSetup(BestOf.Five, PayoutEntries.Five, MoneyEntryFee.Five),
                ChallengeDuration.TwoDays,
                new UtcNowDateTimeProvider()
            );

            for (var i = 0; i < challenge.Setup.Entries; i++)
            {
                challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
            }

            foreach (var participant in challenge.Participants)
            {
                for (var j = 0; j < challenge.Setup.BestOf; j++)
                {
                    var match = new Match(new GameMatchId(Guid.NewGuid()), new UtcNowDateTimeProvider());

                    match.SnapshotStats(challenge.Scoring, faker.MatchStats(Game.LeagueOfLegends));

                    participant.Synchronize(match);
                }
            }

            return challenge;
        }
    }
}
