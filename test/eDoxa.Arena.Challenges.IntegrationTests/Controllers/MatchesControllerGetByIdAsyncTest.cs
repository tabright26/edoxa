// Filename: MatchesControllerGetByIdAsyncTest.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.Abstractions.Repositories;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class MatchesControllerGetByIdAsyncTest
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

        public async Task<HttpResponseMessage> ExecuteAsync(MatchId matchId)
        {
            return await _httpClient
                .DefaultRequestHeaders(
                    new[] {new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()), new Claim(JwtClaimTypes.Role, CustomRoles.Administrator)}
                )
                .GetAsync($"api/matches/{matchId}");
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
        public async Task ShouldBeOk()
        {
            // Arrange
            var challengeRepository = _testServer.GetService<IChallengeRepository>();
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Ended);
            challengeFaker.UseSeed(1);
            var challenge = challengeFaker.Generate();
            challengeRepository.Create(challenge);
            await challengeRepository.CommitAsync();
            var matchId = challenge.Participants.First().Matches.First().Id;

            // Act
            var response = await this.ExecuteAsync(MatchId.FromGuid(matchId));

            // Assert
            response.EnsureSuccessStatusCode();
            var matchViewModel = await response.DeserializeAsync<MatchViewModel>();
            matchViewModel.Should().NotBeNull();
            matchViewModel?.Id.Should().Be(matchViewModel.Id);
        }
    }
}
