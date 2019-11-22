// Filename: ParticipantsControllerGetByIdAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Responses;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.IntegrationTests.Controllers
{
    public sealed class ParticipantsControllerGetByIdAsyncTest : IntegrationTest
    {
        public ParticipantsControllerGetByIdAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ParticipantId participantId)
        {
            return await _httpClient.GetAsync($"api/participants/{participantId}");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(null, Game.LeagueOfLegends, ChallengeState.Ended);
            var challenge = challengeFaker.FakeChallenge();

            var factory = TestApi.WithClaims();
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

            var participantId = challenge.Participants.First().Id;

            // Act
            using var response = await this.ExecuteAsync(ParticipantId.FromGuid(participantId));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var participantResponse = await response.Content.ReadAsAsync<ParticipantResponse>();
            participantResponse.Should().NotBeNull();
            participantResponse?.Id.Should().Be(participantId);
        }
    }
}
