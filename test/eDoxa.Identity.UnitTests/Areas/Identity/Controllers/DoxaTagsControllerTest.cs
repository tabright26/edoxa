﻿// Filename: DoxaTagsControllerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Api.Areas.Identity.Responses;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.TestHelpers;
using eDoxa.Identity.TestHelpers.Fixtures;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    public sealed class DoxaTagsControllerTest : UnitTest
    {
        public DoxaTagsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FetchDoxaTagsAsync()).ReturnsAsync(Array.Empty<UserDoxaTag>()).Verifiable();

            var controller = new DoxaTagsController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.FetchDoxaTagsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            user.DoxaTagHistory = new Collection<UserDoxaTag>
            {
                new UserDoxaTag
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Name = "Test",
                    Code = 1234,
                    Timestamp = DateTime.UtcNow
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FetchDoxaTagsAsync()).ReturnsAsync(user.DoxaTagHistory).Verifiable();

            var controller = new DoxaTagsController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<IEnumerable<UserDoxaTagResponse>>(user.DoxaTagHistory));

            mockUserManager.Verify(userManager => userManager.FetchDoxaTagsAsync(), Times.Once);
        }
    }
}