// Filename: UsersControllerTest.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Functional.Maybe;
using eDoxa.Identity.Api.Controllers;
using eDoxa.Identity.DTO;
using eDoxa.Identity.DTO.Queries;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Identity.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private Mock<IUserQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<IUserQueries>();
        }

        [TestMethod]
        public async Task FindUsersAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var value = new UserListDTO
            {
                Items =
                {
                    new UserDTO()
                }
            };

            _queries.Setup(service => service.FindUsersAsync())
                .ReturnsAsync(new Option<UserListDTO>(value))
                .Verifiable();

            var controller = new UsersController(_queries.Object);

            // Act
            var result = await controller.FindUsersAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }

        [TestMethod]
        public async Task FindUsersAsync_ShouldBeNoContentObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.FindUsersAsync()).ReturnsAsync(new Option<UserListDTO>()).Verifiable();

            var controller = new UsersController(_queries.Object);

            // Act
            var result = await controller.FindUsersAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }
    }
}