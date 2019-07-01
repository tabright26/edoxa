// Filename: ChallengeServiceCloseAsyncTest.cs
// Date Created: 2019-06-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Application.Fakers.Providers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Services
{
    [TestClass]
    public sealed class ChallengeServiceCloseAsyncTest
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
            var challengeRepository = _testServer.GetService<IChallengeRepository>();
            challengeRepository.Create(challenges);
            await challengeRepository.CommitAsync();
            var challengeService = _testServer.GetService<IChallengeService>();
            var closedAt = new FakeDateTimeProvider(DateTime.UtcNow);

            // Act
            await challengeService.CloseAsync(closedAt);

            // Arrange
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenges = await challengeRepository.FetchChallengesAsync(null, ChallengeState.Closed);
            challenges.Should().HaveCount(count);
            challenges.ForEach(challenge => challenge.Timeline.ClosedAt.Should().Be(closedAt.DateTime));
        }
    }
}
