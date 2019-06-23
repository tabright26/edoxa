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

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Common;
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
        private static readonly Faker Faker = new Faker();

        [TestMethod]
        public async Task Create_Challenge_ShouldNotBeNull()
        {
            var challengeFaker = new ChallengeFaker();

            challengeFaker.UseSeed(1);

            var challenge = challengeFaker.Generate();

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
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);

            challengeFaker.UseSeed(1);

            var fakeChallenge = challengeFaker.Generate();

            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    repository.Create(fakeChallenge);

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    challenge.Should().Be(fakeChallenge);

                    challenge.Timeline.State.Should().Be(ChallengeState.Inscription);
                }

                var participant1 = new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider());

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    challenge.Register(participant1);

                    await repository.CommitAsync();

                    challenge.Timeline.State.Should().Be(ChallengeState.Inscription);
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    challenge.Participants.Should().Contain(participant1);

                    challenge.Timeline.State.Should().Be(ChallengeState.Inscription);
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    var participantCount = challenge.Setup.Entries - challenge.Participants.Count;

                    for (var index = 0; index < participantCount; index++)
                    {
                        challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
                    }

                    challenge.Start(new UtcNowDateTimeProvider());

                    await repository.CommitAsync();

                    challenge.Timeline.State.Should().Be(ChallengeState.InProgress);
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    challenge.Participants.Should().HaveCount(challenge.Setup.Entries);

                    challenge.Timeline.State.Should().Be(ChallengeState.InProgress);
                }

                var match1 = new Match(new GameMatchId(Guid.NewGuid()), new UtcNowDateTimeProvider());

                match1.SnapshotStats(fakeChallenge.Scoring, Faker.Match().Stats(ChallengeGame.LeagueOfLegends));

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    var participant = challenge.Participants.Single(p => p == participant1);

                    participant.Synchronize(match1);

                    participant.Synchronize(new UtcNowDateTimeProvider());

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    var participant = challenge.Participants.Single(p => p == participant1);

                    participant.Matches.Should().Contain(match1);

                    participant.SynchronizedAt.Should().NotBeNull();

                    challenge.Timeline.State.Should().Be(ChallengeState.InProgress);
                }

                var match2 = new Match(new GameMatchId(Guid.NewGuid()), new UtcNowDateTimeProvider());

                match2.SnapshotStats(fakeChallenge.Scoring, Faker.Match().Stats(ChallengeGame.LeagueOfLegends));

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    var participant = challenge.Participants.Single(p => p == participant1);

                    participant.Synchronize(match2);

                    participant.Synchronize(new UtcNowDateTimeProvider());

                    await repository.CommitAsync();
                }

                using (var context = factory.CreateContext())
                {
                    var repository = new ChallengeRepository(context, Mapper);

                    var challenge = await repository.FindChallengeAsync(fakeChallenge.Id);

                    var participant = challenge.Participants.Single(p => p == participant1);

                    participant.Matches.Should().BeEquivalentTo(match1, match2);

                    participant.SynchronizedAt.Should().NotBeNull();

                    challenge.Timeline.State.Should().Be(ChallengeState.InProgress);
                }
            }
        }
    }
}
