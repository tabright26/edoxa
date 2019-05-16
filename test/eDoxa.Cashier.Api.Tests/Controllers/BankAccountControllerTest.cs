// Filename: BankAccountControllerTest.cs
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
    public sealed class BankAccountControllerTest
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
            ConstructorTests<BankAccountController>.For(typeof(IMediator))
                .WithName("BankAccountController")
                .WithAttributes(typeof(AuthorizeAttribute), typeof(ApiControllerAttribute), typeof(ApiVersionAttribute), typeof(ProducesAttribute),
                    typeof(RouteAttribute))
                .Assert();
        }

        [TestMethod]
        public async Task CreateBankAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var sourceToken = FakeStripeFactory.CreateSourceToken();

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<CreateBankAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new OkObjectResult(string.Empty)).Verifiable();

            var controller = new BankAccountController(_mockMediator.Object);

            // Act
            var result = await controller.CreateBankAccountAsync(new CreateBankAccountCommand(sourceToken));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<CreateBankAccountCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task DeleteBankAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<DeleteBankAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new OkObjectResult(string.Empty)).Verifiable();

            var controller = new BankAccountController(_mockMediator.Object);

            // Act
            var result = await controller.DeleteBankAccountAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<DeleteBankAccountCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}