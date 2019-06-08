// Filename: ChallengesControllerTest.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ChallengesControllerTest
    {
        private HttpClient _httpClient;
        private ChallengesDbContext _dbContext;

        [TestInitialize]
        public void TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<ChallengesDbContext, Startup>();

            _httpClient = factory.CreateClient();

            _dbContext = factory.DbContext;
        }

        public async Task<HttpResponseMessage> Execute()
        {
            return await _httpClient.GetAsync("api/challenges");
        }

        [TestMethod]
        public async Task ChallengesScenario()
        {
            var response = await this.Execute();

            response.EnsureSuccessStatusCode();

            var challenges = await response.DeserializeAsync<ChallengeViewModel[]>();

            challenges.Should().BeNull();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            _dbContext.Challenges.RemoveRange(_dbContext.Challenges);

            await _dbContext.SaveChangesAsync();
        }
    }
}
