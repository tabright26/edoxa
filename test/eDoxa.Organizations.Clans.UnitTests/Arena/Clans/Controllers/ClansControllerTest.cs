// Filename: ClansControllerTest.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Api.Areas.Clans.Controllers;
using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Organizations.Clans.UnitTests.Arena.Clans.Controllers
{
    [TestClass]
    public class ClansControllerTest
    {
        [TestMethod]
        public async Task GetAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();
            mockClanRepository.Setup(clanRepository => clanRepository.FetchClansAsync()).ReturnsAsync(new List<ClanModel>());

            var clanController = new ClansController(mockClanRepository.Object);

            // Act
            var result = await clanController.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();
        }

        [TestMethod]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockClanRepository = new Mock<IClanRepository>();

            mockClanRepository.Setup(clanRepository => clanRepository.FetchClansAsync())
                .ReturnsAsync(
                    new List<ClanModel>
                    {
                        new ClanModel(),
                        new ClanModel(),
                        new ClanModel()
                    });

            var clanController = new ClansController(mockClanRepository.Object);

            // Act
            var result = await clanController.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
        }
    }
}
