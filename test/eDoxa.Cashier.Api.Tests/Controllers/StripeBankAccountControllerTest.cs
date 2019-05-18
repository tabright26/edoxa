// Filename: StripeBankAccountControllerTest.cs
// Date Created: 2019-05-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Result;
using eDoxa.Seedwork.Domain.Validations;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class StripeBankAccountControllerTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<StripeBankAccountController>.For(typeof(IMediator))
                .WithName("StripeBankAccountController")
                .WithAttributes(typeof(AuthorizeAttribute), typeof(ApiControllerAttribute), typeof(ApiVersionAttribute), typeof(ProducesAttribute),
                    typeof(RouteAttribute), typeof(ApiExplorerSettingsAttribute))
                .Assert();
        }

        [TestMethod]
        public async Task CreateBankAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var sourceToken = FakeStripeFactory.CreateSourceToken();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<CreateBankAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CommandResult.Succeeded)
                .Verifiable();

            var controller = new StripeBankAccountController(_mockMediator.Object);

            // Act
            var result = await controller.CreateBankAccountAsync(new CreateBankAccountCommand(sourceToken));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<CreateBankAccountCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task CreateBankAccountAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var sourceToken = FakeStripeFactory.CreateSourceToken();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<CreateBankAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidationError.Empty)
                .Verifiable();

            var controller = new StripeBankAccountController(_mockMediator.Object);

            // Act
            var result = await controller.CreateBankAccountAsync(new CreateBankAccountCommand(sourceToken));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<CreateBankAccountCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteBankAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockMediator.Setup(mock => mock.Send(It.IsAny<DeleteBankAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CommandResult.Succeeded)
                .Verifiable();

            var controller = new StripeBankAccountController(_mockMediator.Object);

            // Act
            var result = await controller.DeleteBankAccountAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<DeleteBankAccountCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteBankAccountAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            _mockMediator.Setup(mock => mock.Send(It.IsAny<DeleteBankAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(ValidationError.Empty)
                .Verifiable();

            var controller = new StripeBankAccountController(_mockMediator.Object);

            // Act
            var result = await controller.DeleteBankAccountAsync();

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<DeleteBankAccountCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}