// Filename: MatchesControllerGetByIdAsyncTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.IntegrationTests.Controllers
{
    public sealed class MatchesControllerGetByIdAsyncTest : IntegrationTest
    {
        public MatchesControllerGetByIdAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(MatchId matchId)
        {
            return await _httpClient.GetAsync($"api/matches/{matchId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1, Game.LeagueOfLegends, ChallengeState.Ended);

            var challenge = challengeFaker.FakeChallenge();

            var factory = TestHost.WithClaimsFromDefaultAuthentication();
            _httpClient = factory.CreateClient();
            var testServer = factory.Server;
            testServer.CleanupDbContext();

            await testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync(false);
                });

            var matchId = challenge.Participants.First().Matches.First().Id;

            // Act
            using var response = await this.ExecuteAsync(matchId);

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var matchResponse = await response.Content.ReadAsJsonAsync<MatchDto>();
            matchResponse.Should().NotBeNull();
            matchResponse?.Id.Should().Be(matchResponse.Id);
        }
    }
}
