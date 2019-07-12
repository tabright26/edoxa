// Filename: LeagueOfLegendsMatchService.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.LeagueOfLegends;
using eDoxa.Arena.LeagueOfLegends.Dtos;
using eDoxa.Arena.UnitTests.Helpers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using Moq.Protected;

using Newtonsoft.Json;

namespace eDoxa.Arena.UnitTests.LeagueOfLegends
{
    [TestClass]
    public sealed class LeagueOfLegendsMatchService
    {
        [TestMethod]
        public async Task GetMatchReferencesAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var matchReferencesDTO =
                JsonFileConvert.DeserializeObject<IEnumerable<LeagueOfLegendsMatchReferenceDto>>(@"Helpers/Stubs/LeagueOfLegends/MatchReferences.json");

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(
                    () => new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(matchReferencesDTO))
                    }
                )
                .Callback<HttpRequestMessage, CancellationToken>(
                    (request, cancellationToken) =>
                    {
                        request.Method.Should().Be(HttpMethod.Get);
                    }
                );

            var service = new LeagueOfLegendsService(new HttpClient(mockHttpMessageHandler.Object));

            // Act
            var matchReferences = await service.GetMatchReferencesAsync(It.IsAny<string>(), It.IsAny<DateTime>(), It.IsAny<DateTime>());

            // Assert
            matchReferences.Should().BeEquivalentTo(matchReferencesDTO.ToHashSet());

            mockHttpMessageHandler.Verify();
        }

        [TestMethod]
        [DataRow(2973265231)]
        [DataRow(2973293180)]
        [DataRow(2974045372)]
        [DataRow(2974074080)]
        [DataRow(2974102736)]
        public async Task GetMatchAsync_ShouldBeOkObjectResult(long gameId)
        {
            // Arrange
            var matches = JsonFileConvert.DeserializeObject<IEnumerable<LeagueOfLegendsMatchDto>>(@"Helpers/Stubs/LeagueOfLegends/Matches.json");

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(
                    () => new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(matches.Single(x => x.GameId == gameId)))
                    }
                )
                .Callback<HttpRequestMessage, CancellationToken>(
                    (request, cancellationToken) =>
                    {
                        request.Method.Should().Be(HttpMethod.Get);
                    }
                );

            var service = new LeagueOfLegendsService(new HttpClient(mockHttpMessageHandler.Object));

            // Act
            var match = await service.GetMatchAsync(It.IsAny<string>());

            // Assert
            match.Should().BeEquivalentTo(match);

            mockHttpMessageHandler.Verify();
        }
    }
}
