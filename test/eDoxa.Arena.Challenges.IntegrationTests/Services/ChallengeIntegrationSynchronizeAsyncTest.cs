// Filename: ChallengeIntegrationSynchronizeAsyncTestClass.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.Services
{
    public sealed class ChallengeIntegrationSynchronizeAsyncTest : IntegrationTest
    {
        public ChallengeIntegrationSynchronizeAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        // TODO: The method name must be written as a test scenario.
        [Fact]
        public async Task ShouldHaveCountFive()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1, Game.LeagueOfLegends, ChallengeState.InProgress);

            var challenges = challengeFaker.FakeChallenges(5);

            TestApi.CreateClient();
            var testServer = TestApi.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(challenges);
                    await challengeRepository.CommitAsync();
                });

            // Act
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeService = scope.GetRequiredService<IChallengeService>();
                    var synchronizedAt = new DateTimeProvider(DateTime.UtcNow);
                    await challengeService.SynchronizeAsync(Game.LeagueOfLegends, TimeSpan.Zero, synchronizedAt);
                });

            // Arrange
            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challenges = await challengeRepository.FetchChallengesAsync(null, ChallengeState.InProgress);
                    challenges.Should().HaveCount(5);
                });
        }
    }
}
