﻿// Filename: ChallengeParticipantsControllerGetAsyncTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

using Autofac;

using eDoxa.Challenges.Api.HttpClients;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Responses;
using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.Dtos;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.TestHelper.Extensions;
using eDoxa.Seedwork.TestHelper.Http.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.TestHost;

using Moq;

using Xunit;

namespace eDoxa.Challenges.IntegrationTests.Controllers
{
    public sealed class ChallengeParticipantsControllerGetAsyncTest : IntegrationTest
    {
        public ChallengeParticipantsControllerGetAsyncTest(TestApiFixture testApi, TestDataFixture testData, TestMapperFixture testMapper) : base(
            testApi,
            testData,
            testMapper)
        {
        }

        private HttpClient _httpClient;

        private async Task<HttpResponseMessage> ExecuteAsync(ChallengeId challengeId)
        {
            return await _httpClient.GetAsync($"api/challenges/{challengeId}/participants");
        }

        [Fact]
        public async Task ShouldBeHttpStatusCodeOK()
        {
            // Arrange
            var challengeFaker = TestData.FakerFactory.CreateChallengeFaker(1, Game.LeagueOfLegends, ChallengeState.Inscription);

            var challenge = challengeFaker.FakeChallenge();

            var factory = TestApi.WithClaims().WithWebHostBuilder(
                x =>
                {
                    x.ConfigureTestContainer<ContainerBuilder>(
                        y =>
                        {
                            var mock = new Mock<IGamesHttpClient>();

                            mock.Setup(t => t.GetChallengeMatchesAsync(It.IsAny<Game>(), It.IsAny<string>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>())).ReturnsAsync(new List<MatchDto>());

                            y.RegisterInstance(mock.Object).As<IGamesHttpClient>().SingleInstance();
                        });
                });
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

            // Act
            using var response = await this.ExecuteAsync(ChallengeId.FromGuid(challenge.Id));

            // Assert
            response.EnsureSuccessStatusCode();
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            var participantResponses = await response.DeserializeAsync<ParticipantResponse[]>();
            participantResponses.Should().HaveCount(challenge.Participants.Count);
        }
    }
}