// Filename: ParticipantsControllerGetByIdAsyncTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    public sealed class ParticipantsControllerGetByIdAsyncTest : IClassFixture<ArenaChallengesWebApplicationFactory>
    {
        public ParticipantsControllerGetByIdAsyncTest(ArenaChallengesWebApplicationFactory arenaChallengesWebApplicationFactory)
        {
            _httpClient = arenaChallengesWebApplicationFactory.CreateClient();
            _testServer = arenaChallengesWebApplicationFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(ParticipantId participantId)
        {
            return await _httpClient
                .DefaultRequestHeaders(
                    new[] {new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()), new Claim(JwtClaimTypes.Role, CustomRoles.Administrator)}
                )
                .GetAsync($"api/participants/{participantId}");
        }

        [Fact]
        public async Task ShouldBeOk()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Ended);
            var challenge = challengeFaker.Generate();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync();
                }
            );

            var participantId = challenge.Participants.First().Id;

            // Act
            var response = await this.ExecuteAsync(ParticipantId.FromGuid(participantId));

            // Assert
            response.EnsureSuccessStatusCode();
            var participantViewModel = await response.DeserializeAsync<ParticipantViewModel>();
            participantViewModel.Should().NotBeNull();
            participantViewModel?.Id.Should().Be(participantId);
        }
    }
}
