// Filename: AccountControllerTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Testing.MSTest.Constructor;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Tests.Controllers
{
    [TestClass]
    public sealed class AccountControllerTest
    {
        private Mock<IBalanceQuery> _mockAccountQueries;
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockAccountQueries = new Mock<IBalanceQuery>();
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<AccountController>.For(typeof(IBalanceQuery), typeof(IMediator))
                .WithName("AccountController")
                .WithAttributes(
                    typeof(AuthorizeAttribute),
                    typeof(ApiControllerAttribute),
                    typeof(ApiVersionAttribute),
                    typeof(ProducesAttribute),
                    typeof(RouteAttribute),
                    typeof(ApiExplorerSettingsAttribute)
                )
                .Assert();
        }

        [TestMethod]
        public async Task GetBalanceAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockAccountQueries.Setup(mediator => mediator.GetBalanceAsync(It.IsAny<CurrencyType>()))
                .ReturnsAsync(new BalanceDTO())
                .Verifiable();

            var controller = new AccountController(_mockAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetBalanceAsync(null);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockAccountQueries.Verify(mediator => mediator.GetBalanceAsync(It.IsAny<CurrencyType>()), Times.Once);
        }

        [TestMethod]
        public async Task GetBalanceAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            _mockAccountQueries.Setup(mock => mock.GetBalanceAsync(It.IsAny<CurrencyType>())).ReturnsAsync((BalanceDTO) null).Verifiable();

            var controller = new AccountController(_mockAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetBalanceAsync(null);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _mockAccountQueries.Verify(mediator => mediator.GetBalanceAsync(It.IsAny<CurrencyType>()), Times.Once);
        }
    }
}
