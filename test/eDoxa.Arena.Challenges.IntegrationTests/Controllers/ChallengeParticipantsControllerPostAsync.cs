﻿// Filename: ChallengeParticipantsControllerPostAsyncTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Requests;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Helpers;

using IdentityModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ChallengeParticipantsControllerPostAsync : ArenaChallengesWebApplicationFactory
    {
        private HttpClient _httpClient;

        public async Task<HttpResponseMessage> ExecuteAsync(UserId userId, RegisterParticipantRequest request)
        {
            return await _httpClient.DefaultRequestHeaders(new[] {new Claim(JwtClaimTypes.Subject, userId.ToString())})
                .PostAsync($"api/challenges/{request.ChallengeId}/participants", new JsonContent(request));
        }

        [TestMethod]
        public async Task ShouldBeOk()
        {
            // Arrange
            this.WithContainerBuilder(
                builder =>
                {
                    var mock = new Mock<IIdentityService>();

                    mock.Setup(identityService => identityService.HasGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>())).ReturnsAsync(true);

                    mock.Setup(identityService => identityService.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()))
                        .ReturnsAsync(new GameAccountId(Guid.NewGuid().ToString()));

                    builder.RegisterInstance(mock.Object).As<IIdentityService>();
                }
            );
            _httpClient = this.CreateClient();
            await Server.CleanupDbContextAsync();
            var faker = new Faker();
            var userId = faker.User().Id();
            var challengeFaker = new ChallengeFaker(ChallengeGame.LeagueOfLegends, ChallengeState.Inscription);
            challengeFaker.UseSeed(1);
            var challenge = challengeFaker.Generate();

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(userId, new RegisterParticipantRequest(ChallengeId.FromGuid(challenge.Id)));

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
