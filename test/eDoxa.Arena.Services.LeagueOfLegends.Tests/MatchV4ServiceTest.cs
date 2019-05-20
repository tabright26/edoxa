﻿// Filename: MatchV4ServiceTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Services.LeagueOfLegends.DTO.Tests;
using eDoxa.Challenges.Domain.Services.LeagueOfLegends.Api;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;
using Moq.Protected;

using Newtonsoft.Json;

namespace eDoxa.Arena.Services.LeagueOfLegends.Tests
{
    [TestClass]
    public sealed class MatchV4ServiceTest
    {
        [TestMethod]
        public async Task GetMatchReferencesAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var matchReferencesDTO = LeagueOfLegendsDTO.DeserializeMatchReferences();

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
                    (request, cancellationToken) => { request.Method.Should().Be(HttpMethod.Get); }
                );

            var service = new MatchV4Service(new HttpClient(mockHttpMessageHandler.Object), It.IsAny<string>());

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
            var matchDTO = LeagueOfLegendsDTO.DeserializeMatch(gameId);

            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(
                    () => new HttpResponseMessage(HttpStatusCode.OK)
                    {
                        Content = new StringContent(JsonConvert.SerializeObject(matchDTO))
                    }
                )
                .Callback<HttpRequestMessage, CancellationToken>(
                    (request, cancellationToken) => { request.Method.Should().Be(HttpMethod.Get); }
                );

            var service = new MatchV4Service(new HttpClient(mockHttpMessageHandler.Object), It.IsAny<string>());

            // Act
            var match = await service.GetMatchAsync(It.IsAny<string>());

            // Assert
            match.Should().BeEquivalentTo(matchDTO);

            mockHttpMessageHandler.Verify();
        }
    }
}