//// Filename: ChallengesControllerGetByIdAsyncTest.cs
//// Date Created: 2019-09-16
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System.Net;
//using System.Net.Http;
//using System.Threading.Tasks;

//using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
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
//    public sealed class ChallengesControllerGetByIdAsyncTest : IntegrationTest
//    {
//        public ChallengesControllerGetByIdAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
//            testHost,
//            testData,
//            testMapper)
//        {
//        }

//        private HttpClient _httpClient;

//        private async Task<HttpResponseMessage> ExecuteAsync(ChallengeId challengeId)
//        {
//            return await _httpClient.GetAsync($"api/challenges/{challengeId}");
//        }

//        [Fact]
//        public async Task ShouldBeHttpStatusCodeOK()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(null, Game.LeagueOfLegends, ChallengeState.Closed);

//            var challenge = challengeFaker.FakeChallenge();

//            var factory = TestHost.WithClaims();
//            _httpClient = factory.CreateClient();
//            var testServer = factory.Server;
//            testServer.CleanupDbContext();

//            await testServer.UsingScopeAsync(
//                async scope =>
//                {
//                    var challengeRepository = scope.GetRequiredService<IChallengeRepository>();
//                    challengeRepository.Create(challenge);
//                    await challengeRepository.CommitAsync(false);
//                });

//            // Act
//            using var response = await this.ExecuteAsync(ChallengeId.FromGuid(challenge.Id));

//            // Assert
//            response.EnsureSuccessStatusCode();
//            response.StatusCode.Should().Be(HttpStatusCode.OK);
//            var challengeResponse = await response.Content.ReadAsJsonAsync<ChallengeDto>();
//            challengeResponse.Should().NotBeNull();
//            challengeResponse?.Id.Should().Be(challenge.Id);
//        }
//    }
//}
