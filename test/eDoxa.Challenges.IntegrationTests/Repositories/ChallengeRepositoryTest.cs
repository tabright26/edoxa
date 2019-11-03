// Filename: ChallengeRepositoryTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using Bogus;

using eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.IntegrationTests.Repositories
{
    // TODO: These methods must be refactored into smaller tests.
    // TODO: Avoid using Theory in integration tests.
    public sealed class ChallengeRepositoryTest : IntegrationTest
    {
        public ChallengeRepositoryTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(testApi, testData, testMapper)
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

            TestApi.CreateClient();
            var testServer = TestApi.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(fakeChallenge);
                    await challengeRepository.CommitAsync();
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

            var participant1 = new Participant(new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider());

            // Act
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    challenge?.Register(participant1);
                    await challengeRepository.CommitAsync();
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
                        challenge?.Register(new Participant(new UserId(), PlayerId.Parse(Guid.NewGuid().ToString()), new UtcNowDateTimeProvider()));
                    }

                    challenge?.Start(new UtcNowDateTimeProvider());
                    await challengeRepository.CommitAsync();
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

            var match1 = new StatMatch(
                fakeChallenge.Scoring,
                faker.Game().Stats(),
                new GameReference(Guid.NewGuid()),
                new UtcNowDateTimeProvider());

            // Act
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    var participant = challenge?.Participants.Single(p => p == participant1);
                    participant?.Snapshot(match1);
                    await challengeRepository.CommitAsync();
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

            var match2 = new GameMatch(new GameScore(fakeChallenge.Game, 23847883M), new GameReference(Guid.NewGuid()), new UtcNowDateTimeProvider());

            // Act
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    var challenge = await challengeRepository.FindChallengeAsync(fakeChallenge.Id);
                    challenge.Should().NotBeNull();
                    var participant = challenge?.Participants.Single(p => p == participant1);
                    participant?.Snapshot(match2);
                    await challengeRepository.CommitAsync();
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
