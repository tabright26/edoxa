// Filename: ChallengeServiceSynchronizeAsyncTest.cs
// Date Created: 2019-06-25
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

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Providers;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Services
{
    [TestClass]
    public sealed class ChallengeServiceSynchronizeAsyncTest
    {
        private TestServer _testServer;

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new TestArenaChallengesWebApplicationFactory<TestArenaChallengesStartup>();
            factory.CreateClient();
            _testServer = factory.Server;
            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await _testServer.CleanupDbContextAsync();
        }

        [TestMethod]
        public async Task T1()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(ChallengeGame.LeagueOfLegends, ChallengeState.InProgress);
            challengeFaker.UseSeed(1);
            var challenges = challengeFaker.Generate(5) as IEnumerable<IChallenge>;

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetService<IChallengeRepository>();
                    challengeRepository.Create(challenges);
                    await challengeRepository.CommitAsync();
                }
            );

            // Act
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeService = scope.GetService<IChallengeService>();
                    var synchronizedAt = new DateTimeProvider(DateTime.UtcNow);
                    await challengeService.SynchronizeAsync(ChallengeGame.LeagueOfLegends, TimeSpan.Zero, synchronizedAt);
                }
            );

            // Arrange
            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetService<IChallengeRepository>();
                    challenges = await challengeRepository.FetchChallengesAsync(null, ChallengeState.InProgress);
                    challenges.Should().HaveCount(5);
                }
            );
        }
    }
}
