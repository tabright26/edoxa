﻿// Filename: DoxatagHistoryControllerTest.cs
// Date Created: 2019-12-26
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Requests;
using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Identity.UnitTests.Controllers
{
    public sealed class DoxatagHistoryControllerTest : UnitTest
    {
        public DoxatagHistoryControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task ChangeDoxatagAsync_ShouldBeOkObjectResult()
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

            TestMock.UserService.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            TestMock.DoxatagService.Setup(doxatagService => doxatagService.ChangeDoxatagAsync(It.IsAny<User>(), It.IsAny<string>()))
                .ReturnsAsync(DomainValidationResult<Doxatag>.Succeeded(doxatag))
                .Verifiable();

            var controller = new DoxatagHistoryController(TestMock.UserService.Object, TestMock.DoxatagService.Object, TestMapper);

            var request = new ChangeDoxatagRequest
            {
                Name = doxatag.Name
            };

            // Act
            var result = await controller.ChangeDoxatagAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.UserService.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            TestMock.DoxatagService.Verify(doxatagService => doxatagService.ChangeDoxatagAsync(It.IsAny<User>(), It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task FetchDoxatagHistoryAsync_ShouldBeNoContentResult()
        {
            // Arrange
            var user = new User();

            TestMock.UserService.Setup(userManager => userManager.GetUserAsync(It.IsNotNull<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            TestMock.DoxatagService.Setup(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>()))
                .ReturnsAsync(new Collection<Doxatag>())
                .Verifiable();

            var controller = new DoxatagHistoryController(TestMock.UserService.Object, TestMock.DoxatagService.Object, TestMapper);

            // Act
            var result = await controller.FetchDoxatagHistoryAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            TestMock.UserService.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            TestMock.DoxatagService.Verify(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task FetchDoxatagHistoryAsync_ShouldBeOkObjectResult()
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

            var doxatagHistory = new List<Doxatag>
            {
                doxatag
            };

            TestMock.UserService.Setup(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>())).ReturnsAsync(user).Verifiable();

            TestMock.DoxatagService.Setup(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>())).ReturnsAsync(doxatagHistory).Verifiable();

            var controller = new DoxatagHistoryController(TestMock.UserService.Object, TestMock.DoxatagService.Object, TestMapper);

            // Act
            var result = await controller.FetchDoxatagHistoryAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<IEnumerable<DoxatagDto>>(doxatagHistory));

            TestMock.UserService.Verify(userManager => userManager.GetUserAsync(It.IsAny<ClaimsPrincipal>()), Times.Once);

            TestMock.DoxatagService.Verify(doxatagService => doxatagService.FetchDoxatagHistoryAsync(It.IsAny<User>()), Times.Once);
        }
    }
}
