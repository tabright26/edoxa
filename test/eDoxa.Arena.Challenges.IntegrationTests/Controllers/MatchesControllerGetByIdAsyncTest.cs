// Filename: MatchesControllerGetByIdAsyncTest.cs
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
    public sealed class MatchesControllerGetByIdAsyncTest : IClassFixture<ArenaChallengesWebApplicationFactory>
    {
        public MatchesControllerGetByIdAsyncTest(ArenaChallengesWebApplicationFactory arenaChallengesWebApplicationFactory)
        {
            _httpClient = arenaChallengesWebApplicationFactory.CreateClient();
            _testServer = arenaChallengesWebApplicationFactory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(MatchId matchId)
        {
            return await _httpClient
                .DefaultRequestHeaders(
                    new[] {new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString()), new Claim(JwtClaimTypes.Role, CustomRoles.Administrator)}
                )
                .GetAsync($"api/matches/{matchId}");
        }

        [Fact]
        public async Task ShouldBeOk()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Ended);
            challengeFaker.UseSeed(1);
            var challenge = challengeFaker.Generate();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync();
                }
            );

            var matchId = challenge.Participants.First().Matches.First().Id;

            // Act
            var response = await this.ExecuteAsync(MatchId.FromGuid(matchId));

            // Assert
            response.EnsureSuccessStatusCode();
            var matchViewModel = await response.DeserializeAsync<MatchViewModel>();
            matchViewModel.Should().NotBeNull();
            matchViewModel?.Id.Should().Be(matchViewModel.Id);
        }
    }
}
