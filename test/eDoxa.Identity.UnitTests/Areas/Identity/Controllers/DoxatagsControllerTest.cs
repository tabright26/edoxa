// Filename: DoxatagsControllerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Responses;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Areas.Identity.Controllers
{
    public sealed class DoxatagsControllerTest : UnitTest
    {
        public DoxatagsControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FetchDoxatagsAsync()).ReturnsAsync(Array.Empty<UserDoxatag>()).Verifiable();

            var controller = new DoxatagsController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockUserManager.Verify(userManager => userManager.FetchDoxatagsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            user.DoxatagHistory = new Collection<UserDoxatag>
            {
                new UserDoxatag
                {
                    Id = Guid.NewGuid(),
                    UserId = user.Id,
                    Name = "Test",
                    Code = 1234,
                    Timestamp = DateTime.UtcNow
                }
            };

            var mockUserManager = new Mock<IUserManager>();

            mockUserManager.Setup(userManager => userManager.FetchDoxatagsAsync()).ReturnsAsync(user.DoxatagHistory).Verifiable();

            var controller = new DoxatagsController(mockUserManager.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<IEnumerable<UserDoxatagResponse>>(user.DoxatagHistory));

            mockUserManager.Verify(userManager => userManager.FetchDoxatagsAsync(), Times.Once);
        }
    }
}
