// Filename: UsersControllerTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.Domain.Queries;
using eDoxa.Identity.Domain.ViewModels;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Identity.UnitTests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private Mock<IUserQuery> _mockUserQueries;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockUserQueries = new Mock<IUserQuery>();
        }

        [TestMethod]
        public async Task FindUsersAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            _mockUserQueries.Setup(service => service.FindUsersAsync())
                .ReturnsAsync(
                    new List<UserViewModel>
                    {
                        new UserViewModel()
                    }
                )
                .Verifiable();

            var controller = new UsersController(_mockUserQueries.Object);

            // Act
            var result = await controller.FindUsersAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [TestMethod]
        public async Task FindUsersAsync_ShouldBeNoContentObjectResult()
        {
            // Arrange
            _mockUserQueries.Setup(queries => queries.FindUsersAsync()).ReturnsAsync(new List<UserViewModel>()).Verifiable();

            var controller = new UsersController(_mockUserQueries.Object);

            // Act
            var result = await controller.FindUsersAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}
