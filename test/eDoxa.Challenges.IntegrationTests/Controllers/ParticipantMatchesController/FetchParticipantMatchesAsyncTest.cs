﻿// Filename: FetchParticipantMatchesAsyncTest.cs
// Date Created: 2020-01-28
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

namespace eDoxa.Challenges.IntegrationTests.Controllers.ParticipantMatchesController
{
    public sealed class FetchParticipantMatchesAsyncTest : IntegrationTest
    {
        public FetchParticipantMatchesAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testHost,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ParticipantId participantId)
        {
            return await _httpClient.GetAsync($"api/participants/{participantId}/matches");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(null, Game.LeagueOfLegends, ChallengeState.Ended);

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

            var participant = challenge.Participants.First();

            // Act
            using var response = await this.ExecuteAsync(ParticipantId.FromGuid(participant.Id));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var matchResponses = await response.Content.ReadAsJsonAsync<ChallengeMatchDto[]>();
            matchResponses.Should().HaveCount(participant.Matches.Count);
        }
    }
}
