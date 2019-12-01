// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Bogus;

using eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.IntegrationTests.Repositories
{
    // TODO: These methods must be refactored into smaller tests.
    // TODO: Avoid using Theory in integration tests.
    public sealed class ChallengeRepositoryTest : IntegrationTest
    {
        public ChallengeRepositoryTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(testHost, testData, testMapper)
        {
        }

        [Theory]
        [InlineData(56239561)]
        [InlineData(78754231)]
        [InlineData(89785671)]
        public async Task Scenario_ChallengeStateIsValid_ShouldBeTrue(int seed)
        {
            // Arrange
            var faker = new Faker();
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(seed, Game.LeagueOfLegends, ChallengeState.Inscription);
            var fakeChallenge = challengeFaker.FakeChallenge();

            TestHost.CreateClient();
            var testServer = TestHost.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(fakeChallenge);
                    await challengeRepository.CommitAsync(false);
                });

            // Assert
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    challenge?.Should().Be(fakeChallenge);
                    challenge?.Timeline.State.Should().Be(ChallengeState.Inscription);
                });

            var participant1 = new Participant(new ParticipantId(), new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider());

            // Act
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    challenge?.Register(participant1);
                    await challengeRepository.CommitAsync(false);
                });

            // Assert
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    challenge?.Participants.Should().Contain(participant1);
                    challenge?.Timeline.State.Should().Be(ChallengeState.Inscription);
                });

            // Act
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    var entries = challenge?.Entries - challenge?.Participants.Count;

                    for (var index = 0; index < entries; index++)
                    {
                        challenge?.Register(new Participant(new ParticipantId(), new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
                    }

                    challenge?.Start(new UtcNowDateTimeProvider());
                    await challengeRepository.CommitAsync(false);
                });

            // Assert
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    challenge?.Participants.Should().HaveCount(challenge.Entries);
                    challenge?.Timeline.State.Should().Be(ChallengeState.InProgress);
                });

            var match1 = new Match(fakeChallenge.Scoring.Map(faker.Game().Stats()), new GameUuid(Guid.NewGuid()));

            // Act
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    var participant = challenge?.Participants.Single(p => p == participant1);
                    participant?.Snapshot(new List<IMatch> {match1}, new UtcNowDateTimeProvider());
                    await challengeRepository.CommitAsync(false);
                });

            // Assert
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    var participant = challenge?.Participants.Single(p => p == participant1);
                    participant?.Matches.Should().Contain(match1);
                    participant?.SynchronizedAt.Should().NotBeNull();
                    challenge?.Timeline.State.Should().Be(ChallengeState.InProgress);
                });

            var match2 = new Match(
                new List<Stat>
                {
                    new Stat(new StatName(fakeChallenge.Game.Name), new StatValue(23847883M), new StatWeighting(1))
                },
                new GameUuid(Guid.NewGuid()));

            // Act
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    var participant = challenge?.Participants.Single(p => p == participant1);
                    participant?.Snapshot(new List<IMatch> {match2}, new UtcNowDateTimeProvider());
                    await challengeRepository.CommitAsync(false);
                });

            // Assert
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    var participant = challenge?.Participants.Single(p => p == participant1);
                    participant?.Matches.Should().BeEquivalentTo(match1, match2);
                    participant?.SynchronizedAt.Should().NotBeNull();
                    challenge?.Timeline.State.Should().Be(ChallengeState.InProgress);
                });
        }
    }
}
