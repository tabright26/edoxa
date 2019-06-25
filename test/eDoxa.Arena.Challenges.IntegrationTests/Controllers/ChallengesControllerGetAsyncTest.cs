// Filename: ChallengesControllerGetAsyncTest.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ChallengesControllerGetAsyncTest
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

        public async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/challenges");
        }
        
        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<ChallengesDbContext, Startup>();
            _httpClient = factory.CreateClient();
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

        [TestMethod]
        public async Task ShouldBeNoContent()
        {
            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [DataRow(2)]
        [DataRow(5)]
        [DataTestMethod]
        public async Task The_response_http_should_have_exactly_the_same_number_of_fake_challenges_added_to_the_database(int count)
        {
            // Arrange
            var challengeRepository = _testServer.GetService<IChallengeRepository>();
            var challengeFaker = new ChallengeFaker();
            var challenges = challengeFaker.Generate(count);
            challengeRepository.Create(challenges);
            await challengeRepository.CommitAsync();

            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var challengeViewModels = await response.DeserializeAsync<ChallengeViewModel[]>();
            challengeViewModels.Should().HaveCount(count);
        }

        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        [DataTestMethod]
        public async Task Fake_challenge_with_same_seed_should_be_equivalent(int seed)
        {
            // Arrange
            var challengeRepository = _testServer.GetService<IChallengeRepository>();
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);
            var challenge = challengeFaker.Generate();
            challengeRepository.Create(challenge);
            await challengeRepository.CommitAsync();

            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var challengeViewModel = (await response.DeserializeAsync<ChallengeViewModel[]>()).First();
            challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);
            challenge = challengeFaker.Generate();
            challengeViewModel.Id.Should().Be(challenge.Id);
        }
    }
}
