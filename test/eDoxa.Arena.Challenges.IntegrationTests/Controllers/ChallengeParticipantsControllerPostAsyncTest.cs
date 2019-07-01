// Filename: ChallengeParticipantsControllerPostAsyncTest.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Bogus;

using eDoxa.Arena.Challenges.Api;
using eDoxa.Arena.Challenges.Api.Application.Commands;
using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Api.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Testing;
using eDoxa.Seedwork.Testing.TestServer;
using eDoxa.Seedwork.Testing.TestServer.Extensions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ChallengeParticipantsControllerPostAsyncTest
    {
        private HttpClient _httpClient;
        private TestServer _testServer;

        public async Task<HttpResponseMessage> ExecuteAsync(UserId userId, GameAccountId gameAccountId, RegisterParticipantCommand command)
        {
            return await _httpClient
                .DefaultRequestHeaders(
                    new[]
                    {
                        new Claim(JwtClaimTypes.Subject, userId.ToString()),
                        new Claim(ChallengeGame.LeagueOfLegends.GetClaimType(), gameAccountId.ToString())
                    }
                )
                .PostAsync($"api/challenges/{command.ChallengeId}/participants", new JsonContent(command));
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
            var faker = new Faker();
            var userId = faker.UserId();
            var challengeRepository = _testServer.GetService<IChallengeRepository>();
            var challengeFaker = new ChallengeFaker(ChallengeGame.LeagueOfLegends, ChallengeState.Inscription);
            challengeFaker.UseSeed(1);
            var challenge = challengeFaker.Generate();
            challengeRepository.Create(challenge);
            await challengeRepository.CommitAsync();

            // Act
            var response = await this.ExecuteAsync(
                userId,
                faker.Participant().GameAccountId(ChallengeGame.LeagueOfLegends),
                new RegisterParticipantCommand(ChallengeId.FromGuid(challenge.Id))
            );

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
