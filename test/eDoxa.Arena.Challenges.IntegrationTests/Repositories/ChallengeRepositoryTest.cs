// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-06-25
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

using Bogus;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Common;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Repositories
{
    [TestClass]
    public sealed class ChallengeRepositoryTest
    {
        private TestServer _testServer;

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<ChallengesDbContext, Startup>();
            factory.CreateClient();
            _testServer = factory.Server;
            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            var context = _testServer.GetService<ChallengesDbContext>();
            context.Challenges.RemoveRange(context.Challenges);
            await context.SaveChangesAsync();
        }

        [DataTestMethod]
        [DataRow(56239561)]
        [DataRow(78754231)]
        [DataRow(89785671)]
        public async Task Scenario_ChallengeStateIsValid_ShouldBeTrue(int seed)
        {
            // Arrange
            var faker = new Faker();
            var challengeRepository = _testServer.GetService<IChallengeRepository>();
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);
            challengeFaker.UseSeed(seed);
            var fakeChallenge = challengeFaker.Generate();
            challengeRepository.Create(fakeChallenge);
            await challengeRepository.CommitAsync();

            // Assert
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
            challenge.Should().NotBeNull();
            challenge?.Should().Be(fakeChallenge);
            challenge?.Timeline.State.Should().Be(ChallengeState.Inscription);

            // Act
            var participant1 = new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider());
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
            challenge.Should().NotBeNull();
            challenge?.Register(participant1);
            await challengeRepository.CommitAsync();

            // Assert
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
            challenge.Should().NotBeNull();
            challenge?.Participants.Should().Contain(participant1);
            challenge?.Timeline.State.Should().Be(ChallengeState.Inscription);

            // Act
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
            challenge.Should().NotBeNull();
            var entries = challenge?.Setup.Entries - challenge?.Participants.Count;

            for (var index = 0; index < entries; index++)
            {
                challenge?.Register(new Participant(new UserId(), new GameAccountId(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
            }

            challenge?.Start(new UtcNowDateTimeProvider());
            await challengeRepository.CommitAsync();

            // Assert
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
            challenge.Should().NotBeNull();
            challenge?.Participants.Should().HaveCount(challenge.Setup.Entries);
            challenge?.Timeline.State.Should().Be(ChallengeState.InProgress);

            // Act
            var match1 = new Match(new GameReference(Guid.NewGuid()), new UtcNowDateTimeProvider());
            match1.Snapshot(faker.Match().Stats(ChallengeGame.LeagueOfLegends), fakeChallenge.Scoring);
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
            challenge.Should().NotBeNull();
            var participant = challenge?.Participants.Single(p => p == participant1);
            participant?.Snapshot(match1);
            await challengeRepository.CommitAsync();

            // Assert
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
            challenge.Should().NotBeNull();
            participant = challenge?.Participants.Single(p => p == participant1);
            participant?.Matches.Should().Contain(match1);
            participant?.SynchronizedAt.Should().NotBeNull();
            challenge?.Timeline.State.Should().Be(ChallengeState.InProgress);

            // Act
            var match2 = new Match(new GameReference(Guid.NewGuid()), new UtcNowDateTimeProvider());
            match2.Snapshot(faker.Match().Stats(ChallengeGame.LeagueOfLegends), fakeChallenge.Scoring);
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
            challenge.Should().NotBeNull();
            participant = challenge?.Participants.Single(p => p == participant1);
            participant?.Snapshot(match2);
            await challengeRepository.CommitAsync();

            // Assert
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
            challenge.Should().NotBeNull();
            participant = challenge?.Participants.Single(p => p == participant1);
            participant?.Matches.Should().BeEquivalentTo(match1, match2);
            participant?.SynchronizedAt.Should().NotBeNull();
            challenge?.Timeline.State.Should().Be(ChallengeState.InProgress);
        }
    }
}
