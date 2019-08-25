// Filename: ChallengeHistoryControllerGetAsyncTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;
using FluentAssertions;
using IdentityModel;

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    public sealed class ChallengeHistoryControllerGetAsyncTest : IClassFixture<ArenaChallengesWebApiFactory>
    {
        public ChallengeHistoryControllerGetAsyncTest(ArenaChallengesWebApiFactory factory)
        {
            _factory = factory;
        }

        private readonly ArenaChallengesWebApiFactory _factory;

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ChallengeGame game = null, ChallengeState state = null)
        {
            return await _httpClient.GetAsync($"api/challenges/history?game={game}&state={state}");
        }

        [Fact]
        public async Task ShouldBeOk()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.InProgress);
            challengeFaker.UseSeed(1);
            var challenge = challengeFaker.Generate();

            var participant = challenge.Participants.First();

            var factory = _factory.WithClaims(new Claim(JwtClaimTypes.Subject, participant.UserId.ToString()));

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            await server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var challengeViewModel = (await response.DeserializeAsync<ChallengeViewModel[]>()).First();
            challengeViewModel.Id.Should().Be(challenge.Id);
        }
    }
}
