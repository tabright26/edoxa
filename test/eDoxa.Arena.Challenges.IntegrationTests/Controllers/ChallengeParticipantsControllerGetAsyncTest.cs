// Filename: ChallengeParticipantsControllerGetAsyncTest.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Arena.Challenges.Infrastructure.Extensions;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ChallengeParticipantsControllerGetAsyncTest
    {
        private HttpClient _httpClient;
        private ChallengesDbContext _dbContext;

        public async Task<HttpResponseMessage> ExecuteAsync(ChallengeId challengeId)
        {
            return await _httpClient.DefaultRequestHeaders(new[] {new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString())})
                .GetAsync($"api/challenges/{challengeId}/participants");
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            var factory = new CustomWebApplicationFactory<ChallengesDbContext, Startup>();

            _httpClient = factory.CreateClient();

            _dbContext = factory.DbContext;

            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            _dbContext.Challenges.RemoveRange(_dbContext.Challenges);

            await _dbContext.SaveChangesAsync();
        }

        [TestMethod]
        public async Task ShouldBeOk()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);
            challengeFaker.UseSeed(1);
            var challenge = challengeFaker.GenerateModel();
            _dbContext.Challenges.Add(challenge);
            await _dbContext.SaveChangesAsync();

            // Act
            var response = await this.ExecuteAsync(ChallengeId.FromGuid(challenge.Id));

            // Assert
            response.EnsureSuccessStatusCode();
            var participantViewModels = await response.DeserializeAsync<ParticipantViewModel[]>();
            participantViewModels.Should().HaveCount(challenge.Participants.Count);
        }
    }
}
