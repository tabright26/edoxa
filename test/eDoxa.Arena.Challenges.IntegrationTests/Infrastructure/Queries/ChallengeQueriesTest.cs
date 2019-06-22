// Filename: ChallengeQueriesTest.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using AutoMapper;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Queries;
using eDoxa.Arena.Challenges.Infrastructure.Repositories;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Match = eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Match;

namespace eDoxa.Arena.Challenges.IntegrationTests.Infrastructure.Queries
{
    [TestClass]
    public sealed class ChallengeQueriesTest
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
                    var mock = new Mock<IHttpContextAccessor>();

                    mock.SetupClaims();

                    var challengeQuery = new ChallengeQuery(context, Mapper, mock.Object);

                    var challengeViewModel = await challengeQuery.FindChallengeAsync(challenge.Id);

                    challengeViewModel.Should().NotBeNull();

                    //challenge.Should().NotBeNull();

                    //challenge.ShouldBeValidObjectState();

                    //var challengeAsNoTracking = await repository.FindChallengeAsync(challenge.Id);

                    //challengeAsNoTracking.Should().NotBeNull();

                    //challengeAsNoTracking.ShouldBeValidObjectState();

                    //challengeAsNoTracking.Should().BeEquivalentTo(challenge);
                }
            }
        }

        private static Challenge CreateChallenge()
        {
            var faker = new Faker();

            var challenge = new FakeChallenge(
                ChallengeGame.LeagueOfLegends,
                new ChallengeName("Challenge"),
                new ChallengeSetup(BestOf.Five, PayoutEntries.Five, MoneyEntryFee.Five),
                ChallengeDuration.TwoDays,
                new UtcNowDateTimeProvider()
            );

            for (var i = 0; i < challenge.Setup.Entries; i++)
            {
                challenge.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
            }

            challenge.Start(new UtcNowDateTimeProvider());

            foreach (var participant in challenge.Participants)
            {
                for (var j = 0; j < challenge.Setup.BestOf; j++)
                {
                    var match = new Match(new GameMatchId(Guid.NewGuid()), new UtcNowDateTimeProvider());

                    match.SnapshotStats(challenge.Scoring, faker.Match().Stats(ChallengeGame.LeagueOfLegends));

                    participant.Synchronize(match);
                }
            }

            return challenge;
        }

        private sealed class FakeChallenge : Challenge
        {
            public FakeChallenge(
                ChallengeGame game,
                ChallengeName name,
                ChallengeSetup setup,
                ChallengeDuration duration,
                IDateTimeProvider createdAt
            ) : base(
                name,
                game,
                setup,
                duration,
                createdAt
            )
            {
            }
        }
    }
}
