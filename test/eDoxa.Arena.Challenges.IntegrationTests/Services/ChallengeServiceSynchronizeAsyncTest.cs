// Filename: ChallengeServiceSynchronizeAsyncTest.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.Services
{
    public sealed class ChallengeServiceSynchronizeAsyncTest : IClassFixture<ArenaChallengeApiFactory>
    {
        public ChallengeServiceSynchronizeAsyncTest(ArenaChallengeApiFactory arenaChallengeApiFactory)
        {
            arenaChallengeApiFactory.CreateClient();
            _testServer = arenaChallengeApiFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly TestServer _testServer;

        // TODO: The method name must be written as a test scenario.
        [Fact]
        public async Task ShouldHaveCountFive()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(ChallengeGame.LeagueOfLegends, ChallengeState.InProgress);
            challengeFaker.UseSeed(1);
            var challenges = challengeFaker.Generate(5) as IEnumerable<IChallenge>;

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(challenges);
                    await challengeRepository.CommitAsync();
                }
            );

            // Act
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeService = scope.GetRequiredService<IChallengeService>();
                    var synchronizedAt = new DateTimeProvider(DateTime.UtcNow);
                    await challengeService.SynchronizeAsync(ChallengeGame.LeagueOfLegends, TimeSpan.Zero, synchronizedAt);
                }
            );

            // Arrange
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challenges = await challengeRepository.FetchChallengesAsync(null, ChallengeState.InProgress);
                    challenges.Should().HaveCount(5);
                }
            );
        }
    }
}
