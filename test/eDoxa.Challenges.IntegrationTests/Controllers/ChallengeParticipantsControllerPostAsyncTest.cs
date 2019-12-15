﻿//// Filename: ChallengeParticipantsControllerPostAsyncTest.cs
//// Date Created: 2019-10-06
//// 
//// ================================================
//// Copyright © 2019, eDoxa. All rights reserved.

//using System;
//using System.Net;
//using System.Net.Http;
//using System.Security.Claims;
//using System.Threading.Tasks;

//using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
//using eDoxa.Challenges.Domain.Repositories;
//using eDoxa.Challenges.TestHelper;
//using eDoxa.Challenges.TestHelper.Fixtures;
//using eDoxa.Grpc.Protos.Challenges.Requests;
//using eDoxa.Seedwork.Application.Extensions;
//using eDoxa.Seedwork.Domain.Misc;
//using eDoxa.Seedwork.TestHelper.Extensions;

//using FluentAssertions;

//using IdentityModel;

//using Xunit;

//namespace eDoxa.Challenges.IntegrationTests.Controllers
//{
//    public sealed class ChallengeParticipantsControllerPostAsyncTest : IntegrationTest
//    {
//        public ChallengeParticipantsControllerPostAsyncTest(TestHostFixture testHost, TestDataFixture testData, TestMapperFixture testMapper) : base(
//            testHost,
//            testData,
//            testMapper)
//        {
//        }

//        private HttpClient _httpClient;

//        private async Task<HttpResponseMessage> ExecuteAsync(ChallengeId challengeId, RegisterChallengeParticipantRequest request)
//        {
//            return await _httpClient.PostAsJsonAsync($"api/challenges/{challengeId}/participants", request);
//        }

//        [Fact]
//        public async Task ShouldBeHttpStatusCodeOK()
//        {
//            // Arrange
//            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1, Game.LeagueOfLegends, ChallengeState.Inscription);

//            var challenge = challengeFaker.FakeChallenge();

//            var participantId = new ParticipantId();
//            var userId = new UserId();
//            var playerId = PlayerId.Parse(Guid.NewGuid().ToString());

//            // Need extension methods for complex claims.
//            var factory = TestHost.WithClaims(new Claim(JwtClaimTypes.Subject, userId.ToString()), new Claim($"games/{challenge.Game.NormalizedName}", playerId));

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
//            using var response = await this.ExecuteAsync(challenge.Id, new RegisterChallengeParticipantRequest
//            {
//                ParticipantId = participantId
//            });

//            // Assert
//            response.EnsureSuccessStatusCode();
//            response.StatusCode.Should().Be(HttpStatusCode.OK);
//        }
//    }
//}
