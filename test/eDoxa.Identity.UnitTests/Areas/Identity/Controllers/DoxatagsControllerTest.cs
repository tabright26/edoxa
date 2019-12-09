// Filename: DoxatagsControllerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.Identity.Api.Areas.Identity.Controllers;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Responses;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

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
            var mockDoxatagService = new Mock<IDoxatagService>();

            mockDoxatagService.Setup(doxatagService => doxatagService.FetchDoxatagsAsync()).ReturnsAsync(Array.Empty<Doxatag>()).Verifiable();

            var controller = new DoxatagsController(mockDoxatagService.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockDoxatagService.Verify(doxatagService => doxatagService.FetchDoxatagsAsync(), Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var user = new User
            {
                Id = Guid.NewGuid()
            };

            var doxatag = new Doxatag(
                UserId.FromGuid(user.Id),
                "Name",
                1000,
                new UtcNowDateTimeProvider());

            var doxatagHistory = new List<Doxatag> {doxatag};

            var mockDoxatagService = new Mock<IDoxatagService>();

            mockDoxatagService.Setup(doxatagService => doxatagService.FetchDoxatagsAsync()).ReturnsAsync(doxatagHistory).Verifiable();

            var controller = new DoxatagsController(mockDoxatagService.Object, TestMapper);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<IEnumerable<DoxatagResponse>>(doxatagHistory));

            mockDoxatagService.Verify(doxatagService => doxatagService.FetchDoxatagsAsync(), Times.Once);
        }
    }
}
