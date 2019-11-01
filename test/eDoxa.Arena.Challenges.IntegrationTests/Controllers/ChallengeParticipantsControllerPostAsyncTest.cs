// Filename: ChallengeParticipantsControllerPostAsyncTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.RefitClients;
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

using Match = eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.Match;

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
            var playerId = PlayerId.Parse(Guid.NewGuid().ToString());

            // Need extension methods for complex claims.
            var factory = TestApi.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim($"games/{challenge.Game.NormalizedName}", playerId))
                .WithWebHostBuilder(
                    x =>
                    {
                        x.ConfigureTestContainer<ContainerBuilder>(
                            t =>
                            {
                                var mock = new Mock<IGamesApiRefitClient>();

                                mock.Setup(g => g.GetMatchesAsync(It.IsAny<PlayerId>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
                                    .ReturnsAsync(new List<Match>());

                                t.RegisterInstance(mock.Object).As<IGamesApiRefitClient>().SingleInstance();
                            });
                    });

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
