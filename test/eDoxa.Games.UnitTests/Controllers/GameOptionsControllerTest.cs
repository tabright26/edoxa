﻿// Filename: GameOptionsControllerTest.cs
// Date Created: 2019-11-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Games.Abstractions.Services;
using eDoxa.Games.Api.Controllers;
using eDoxa.Games.Api.Infrastructure;
using eDoxa.Games.TestHelper;
using eDoxa.Games.TestHelper.Fixtures;
using eDoxa.Games.TestHelper.Mocks;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

using Moq;

using Xunit;

namespace eDoxa.Games.UnitTests.Controllers
{
    public sealed class GameOptionsControllerTest : UnitTest
    {
        public GameOptionsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var serviceCredential = new Mock<ICredentialService>();

            var gameOptionsController = new GameOptionsController(serviceCredential.Object, new OptionsWrapper<GamesOptions>(new GamesOptions()));

            var mockHttpContextAccessor = new MockHttpContextAccessor();

            gameOptionsController.ControllerContext.HttpContext = mockHttpContextAccessor.Object.HttpContext;

            // Act
            var result = gameOptionsController.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
