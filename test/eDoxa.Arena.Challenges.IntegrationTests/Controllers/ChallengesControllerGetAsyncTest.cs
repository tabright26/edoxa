// Filename: ChallengesControllerGetAsyncTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers;
using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.IntegrationTests.Helpers;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Testing.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.IntegrationTests.Controllers
{
    [TestClass]
    public sealed class ChallengesControllerGetAsyncTest : ArenaChallengesWebApplicationFactory
    {
        private HttpClient _httpClient;

        public async Task<HttpResponseMessage> ExecuteAsync()
        {
            return await _httpClient.GetAsync("api/challenges");
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
        public async Task ShouldBeNoContent()
        {
            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
        }

        [DataRow(2)]
        [DataRow(5)]
        [DataTestMethod]
        public async Task The_response_http_should_have_exactly_the_same_number_of_fake_challenges_added_to_the_database(int count)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            var challenges = challengeFaker.Generate(count);

            await Server.UsingScopeAsync(
                async scope =>
                {
                    var challengeRepository = scope.GetService<IChallengeRepository>();
                    challengeRepository.Create(challenges);
                    await challengeRepository.CommitAsync();
                }
            );

            // Act
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var challengeViewModels = await response.DeserializeAsync<ChallengeViewModel[]>();
            challengeViewModels.Should().HaveCount(count);
        }

        [DataRow(100)]
        [DataRow(1000)]
        [DataRow(10000)]
        [DataTestMethod]
        public async Task Fake_challenge_with_same_seed_should_be_equivalent(int seed)
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);
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
            var response = await this.ExecuteAsync();

            // Assert
            response.EnsureSuccessStatusCode();
            var challengeViewModel = (await response.DeserializeAsync<ChallengeViewModel[]>()).First();
            challengeFaker = new ChallengeFaker();
            challengeFaker.UseSeed(seed);
            challenge = challengeFaker.Generate();
            challengeViewModel.Id.Should().Be(challenge.Id);
        }
    }
}
