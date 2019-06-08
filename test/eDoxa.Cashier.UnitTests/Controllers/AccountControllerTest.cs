// Filename: AccountControllerTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Abstractions;
using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Api.ViewModels;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Seedwork.Testing.Constructor;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Controllers
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
            _mockAccountQueries.Setup(mediator => mediator.GetBalanceAsync(It.IsAny<CurrencyType>())).ReturnsAsync(new BalanceViewModel()).Verifiable();

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
            _mockAccountQueries.Setup(mock => mock.GetBalanceAsync(It.IsAny<CurrencyType>())).ReturnsAsync((BalanceViewModel) null).Verifiable();

            var controller = new AccountController(_mockAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetBalanceAsync(null);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _mockAccountQueries.Verify(mediator => mediator.GetBalanceAsync(It.IsAny<CurrencyType>()), Times.Once);
        }
    }
}
