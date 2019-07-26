// Filename: ChallengeParticipantsControllerGetAsyncTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using IdentityModel;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ChallengeParticipantsControllerGetAsyncTest : ArenaChallengesWebApplicationFactory
    {
        private HttpClient _httpClient;

        public async Task<HttpResponseMessage> ExecuteAsync(ChallengeId challengeId)
        {
            return await _httpClient.DefaultRequestHeaders(new[] {new Claim(JwtClaimTypes.Subject, Guid.NewGuid().ToString())})
                .GetAsync($"api/challenges/{challengeId}/participants");
        }

        [TestInitialize]
        public async Task TestInitialize()
        {
            _httpClient = this.CreateClient();
            
            await this.TestCleanup();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            await Server.CleanupDbContextAsync();
        }

        [TestMethod]
        public async Task ShouldBeOk()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker(state: ChallengeState.Inscription);
            challengeFaker.UseSeed(1);
            var challenge = challengeFaker.Generate();

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetService<IChallengeRepository>();
                    challengeRepository.Create(challenge);
                    await challengeRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync(ChallengeId.FromGuid(challenge.Id));

            // Assert
            response.EnsureSuccessStatusCode();
            var participantViewModels = await response.DeserializeAsync<ParticipantViewModel[]>();
            participantViewModels.Should().HaveCount(challenge.Participants.Count);
        }
    }
}
