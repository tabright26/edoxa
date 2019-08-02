// Filename: ChallengeParticipantsControllerPostAsyncTest.cs
// Date Created: 2019-07-26
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
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Helpers;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    public sealed class ChallengeParticipantsControllerPostAsyncTest : IClassFixture<ArenaChallengesWebApplicationFactory>
    {
        public ChallengeParticipantsControllerPostAsyncTest(ArenaChallengesWebApplicationFactory arenaChallengesWebApplicationFactory)
        {
            var factory = arenaChallengesWebApplicationFactory.WithWebHostBuilder(
                builder => builder.ConfigureTestContainer<ContainerBuilder>(
                    container =>
                    {
                        var mock = new Mock<IIdentityService>();

                        mock.Setup(identityService => identityService.HasGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>())).ReturnsAsync(true);

                        mock.Setup(identityService => identityService.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<ChallengeGame>()))
                            .ReturnsAsync(new GameAccountId(Guid.NewGuid().ToString()));

                        container.RegisterInstance(mock.Object).As<IIdentityService>();
                    }
                )
            );

            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(UserId userId, RegisterParticipantRequest request)
        {
            return await _httpClient.DefaultRequestHeaders(new[] {new Claim(JwtClaimTypes.Subject, userId.ToString())})
                .PostAsync($"api/challenges/{request.ChallengeId}/participants", new JsonContent(request));
        }

        [Fact]
        public async Task ShouldBeOk()
        {
            // Arrange
            var faker = new Faker();
            var userId = faker.User().Id();
            var challengeFaker = new ChallengeFaker(ChallengeGame.LeagueOfLegends, ChallengeState.Inscription);
            challengeFaker.UseSeed(1);
            var challenge = challengeFaker.Generate();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(userId, new RegisterParticipantRequest(ChallengeId.FromGuid(challenge.Id)));

            // Assert
            response.EnsureSuccessStatusCode();
        }
    }
}
