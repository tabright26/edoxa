// Filename: ChallengeHistoryControllerGetAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Responses;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using IdentityModel;

using Xunit;

namespace eDoxa.Challenges.IntegrationTests.Controllers
{
    public sealed class ChallengeHistoryControllerGetAsyncTest : IntegrationTest
    {
        public ChallengeHistoryControllerGetAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(Game game = null, ChallengeState state = null)
        {
            return await _httpClient.GetAsync($"api/challenges/history?game={game}&state={state}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1, Game.LeagueOfLegends, ChallengeState.InProgress);

            var challenge = challengeFaker.FakeChallenge();

            var participant = challenge.Participants.First();

            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, participant.UserId.ToString()));

            _httpClient = factory.CreateClient();
            var server = factory.Server;
            server.CleanupDbContext();

            await server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync(false);
                });

            // Act
            using var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var challengeResponse = (await response.Content.ReadAsAsync<ChallengeResponse[]>()).First();
            challengeResponse.Id.Should().Be(challenge.Id.ToGuid());
        }
    }
}
