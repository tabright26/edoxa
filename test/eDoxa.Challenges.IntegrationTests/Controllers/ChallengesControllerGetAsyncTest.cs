//// Filename: ChallengesControllerGetAsyncTest.cs
//// Date Created: 2019-09-16
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;

//using eDoxa.Challenges.Domain.Repositories;
//using eDoxa.Challenges.TestHelper;
//using eDoxa.Challenges.TestHelper.Fixtures;
//using eDoxa.Grpc.Protos.Challenges.Dtos;
//using eDoxa.Seedwork.Application.Extensions;
//using eDoxa.Seedwork.Domain.Misc;
//using eDoxa.Seedwork.TestHelper.Extensions;

//using FluentAssertions;

//using Xunit;

//namespace eDoxa.Challenges.IntegrationTests.Controllers
//{
//    public sealed class ChallengesControllerGetAsyncTest : IntegrationTest
//    {
//        public ChallengesControllerGetAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
//            testHost,
//            testData,
//            testMapper)
//        {
//        }

//        private HttpClient _httpClient;

//        private async Task<HttpResponseMessage> ExecuteAsync()
//        {
//            return await _httpClient.GetAsync("api/challenges");
//        }

//        [Fact]
//        public async Task ShouldBeHttpStatusCodeNoContent()
//        {
//            // Arrange
//            _httpClient = TestHost.CreateClient();
//            var testServer = TestHost.Server;
//            testServer.CleanupDbContext();

//            // Act
//            using var response = await this.ExecuteAsync();

//            // Assert
//            response.EnsureSuccessStatusCode();
//            response.StatusCode.Should().Be(HttpStatusCode.NoContent);
//        }

//        [Fact]
//        public async Task ShouldBeHttpStatusCodeOK()
//        {
//            // Arrange
//            const int count = 5;

//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1000, Game.LeagueOfLegends);

//            var challenges = challengeFaker.FakeChallenges(count);

//            _httpClient = TestHost.CreateClient();
//            var testServer = TestHost.Server;
//            testServer.CleanupDbContext();

//            await testServer.UsingScopeAsync(
//                async scope =>
//                {
//                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
//                    challengeRepository.Create(challenges);
//                    await challengeRepository.CommitAsync(false);
//                });

//            // Act
//            using var response = await this.ExecuteAsync();

//            // Assert
//            response.EnsureSuccessStatusCode();
//            response.StatusCode.Should().Be(HttpStatusCode.OK);
//            var challengeResponses = await response.Content.ReadAsJsonAsync<ChallengeDto[]>();
//            challengeResponses.Should().HaveCount(count);
//        }
//    }
//}
