// Filename: MatchesControllerGetByIdAsyncTest.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Areas.Challenges.Responses;
using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;
using eDoxa.Seedwork.Testing.Http.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    public sealed class MatchesControllerGetByIdAsyncTest : IClassFixture<ArenaChallengeApiFactory>
    {
        public MatchesControllerGetByIdAsyncTest(ArenaChallengeApiFactory arenaChallengeApiFactory)
        {
            var factory = arenaChallengeApiFactory.WithClaims();
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(MatchId matchId)
        {
            return await _httpClient.GetAsync($"api/matches/{matchId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Ended);
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

            var matchId = challenge.Participants.First().Matches.First().Id;

            // Act
            using var response = await this.ExecuteAsync(matchId);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var matchResponse = await response.DeserializeAsync<MatchResponse>();
            matchResponse.Should().NotBeNull();
            matchResponse?.Id.Should().Be(matchResponse.Id);
        }
    }
}
