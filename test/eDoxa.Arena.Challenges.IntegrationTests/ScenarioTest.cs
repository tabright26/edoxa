// Filename: ScenarioTest.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests
{
    [TestClass]
    public sealed class ScenarioTest
    {
        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        public ScenarioTest()
        {
            // The order of the method calls is sequential.
            var factory = new CustomWebApplicationFactory();

            _httpClient = factory.CreateClient();

            _testServer = factory.Server;
        }

        [TestMethod]
        public async Task ChallengesScenario()
        {
            _testServer.MigrateDbContext<ChallengesDbContext, ScenarioDbContextData>();

            var response = await _httpClient.GetAsync("api/challenges");

            response.EnsureSuccessStatusCode();

            var challenges = await response.DeserializeAsync<ChallengeDTO[]>();

            challenges.Should().BeNull();
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _testServer.CleanupDbContext<ChallengesDbContext>();
        }
    }
}
