﻿// Filename: ChallengeServiceSynchronizeAsyncTest.cs
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

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Application.Fakers.Providers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

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

        //[TestMethod]
        //public async Task Test()
        //{
        //    // Arrange
        //    var testServer = TestServerHelper.CreateTestServer<ChallengesDbContext>();


        //}

        [TestMethod]
        public async Task T1()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(ChallengeGame.LeagueOfLegends, ChallengeState.InProgress);
            challengeFaker.UseSeed(1);
            var challenges = challengeFaker.Generate(5) as IEnumerable<IChallenge>;
            var challengeRepository = _testServer.GetService<IChallengeRepository>();
            challengeRepository.Create(challenges);
            await challengeRepository.CommitAsync();
            var challengeService = _testServer.GetService<IChallengeService>();
            var synchronizedAt = new FakeDateTimeProvider(DateTime.UtcNow);

            // Act
            await challengeService.SynchronizeAsync(synchronizedAt, ChallengeGame.LeagueOfLegends);

            // Arrange
            challengeRepository = _testServer.GetService<IChallengeRepository>();
            challenges = await challengeRepository.FindChallengesAsync(null, ChallengeState.InProgress);
            challenges.Should().HaveCount(5);
        }
    }
}
