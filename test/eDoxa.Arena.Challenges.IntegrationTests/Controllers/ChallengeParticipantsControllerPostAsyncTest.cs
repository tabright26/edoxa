// Filename: ChallengeParticipantsControllerPostAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.TestHelper;
using eDoxa.Arena.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

using Claim = System.Security.Claims.Claim;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    public sealed class ChallengeParticipantsControllerPostAsyncTest : IntegrationTest
    {
        public ChallengeParticipantsControllerPostAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ChallengeId challengeId)
        {
            return await _httpClient.PostAsync($"api/challenges/{challengeId}/participants", null);
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1, Game.LeagueOfLegends, ChallengeState.Inscription);

            var challenge = challengeFaker.FakeChallenge();

            var userId = new UserId();
            var gameAccountId = new GameAccountId(Guid.NewGuid().ToString());

            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()))
                .WithWebHostBuilder(
                    builder => builder.ConfigureTestContainer<ContainerBuilder>(
                        container =>
                        {
                            var mock = new Mock<IIdentityService>();

                            mock.Setup(identityService => identityService.HasGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                                .ReturnsAsync(true);

                            mock.Setup(identityService => identityService.GetGameAccountIdAsync(It.IsAny<UserId>(), It.IsAny<Game>()))
                                .ReturnsAsync(gameAccountId);

                            container.RegisterInstance(mock.Object).As<IIdentityService>();
                        }));

            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync();
                });

            // Act
            using var response = await this.ExecuteAsync(ChallengeId.FromGuid(challenge.Id));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
