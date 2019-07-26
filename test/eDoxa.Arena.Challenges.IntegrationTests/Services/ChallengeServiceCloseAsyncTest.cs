// Filename: ChallengeServiceCloseAsyncTest.cs
// Date Created: 2019-06-25
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
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Providers;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Services
{
    [TestClass]
    public sealed class ChallengeServiceCloseAsyncTest : ArenaChallengesWebApplicationFactory
    {
        [TestInitialize]
        public async Task TestInitialize()
        {
            this.CreateClient();

            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await Server.CleanupDbContextAsync();
        }

        [DataRow(5, 1)]
        [DataRow(6, 2)]
        [DataRow(7, 4)]
        [DataRow(8, 8)]
        [DataTestMethod]
        public async Task ShouldBeValid(int count, int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Ended);
            challengeFaker.UseSeed(seed);
            var challenges = challengeFaker.Generate(count) as IEnumerable<IChallenge>;

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetService<IChallengeRepository>();
                    challengeRepository.Create(challenges);
                    await challengeRepository.CommitAsync();
                }
            );

            var closedAt = new DateTimeProvider(DateTime.UtcNow);

            // Act
            await Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeService = scope.GetService<IChallengeService>();
                    await challengeService.CloseAsync(closedAt);
                }
            );

            // Arrange
            await Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetService<IChallengeRepository>();
                    challenges = await challengeRepository.FetchChallengesAsync(null, ChallengeState.Closed);
                    challenges.Should().HaveCount(count);
                    challenges.ForEach(challenge => challenge.Timeline.ClosedAt.Should().Be(closedAt.DateTime));
                }
            );
        }
    }
}
