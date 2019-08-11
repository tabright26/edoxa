// Filename: ChallengesControllerGetByIdAsyncTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Xunit;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    public sealed class ChallengesControllerGetByIdAsyncTest : IClassFixture<ArenaChallengesWebApplicationFactory>
    {
        public ChallengesControllerGetByIdAsyncTest(ArenaChallengesWebApplicationFactory arenaChallengesWebApplicationFactory)
        {
            var factory = arenaChallengesWebApplicationFactory.WithClaimsPrincipal();
            _httpClient = factory.CreateClient();
            _testServer = factory.Server;
            _testServer.CleanupDbContext();
        }

        private readonly HttpClient _httpClient;
        private readonly TestServer _testServer;

        private async Task<HttpResponseMessage> ExecuteAsync(ChallengeId challengeId)
        {
            return await _httpClient.GetAsync($"api/challenges/{challengeId}");
        }

        [Fact]
        public async Task ShouldBeOk()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Closed);
            var challenge = challengeFaker.Generate();

            await _testServer.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync();
                }
            );

            // Act
            using var response = await this.ExecuteAsync(ChallengeId.FromGuid(challenge.Id));

            // Assert
            response.EnsureSuccessStatusCode();
            var challengeViewModel = await response.DeserializeAsync<ChallengeViewModel>();
            challengeViewModel.Should().NotBeNull();
            challengeViewModel?.Id.Should().Be(challenge.Id);
        }
    }
}
