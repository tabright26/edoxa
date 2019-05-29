// Filename: StripeAccountControllerTest.cs
// Date Created: 2019-05-28
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
using eDoxa.Testing.MSTest;

using FluentAssertions;

using FluentValidation.Results;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Tests.Controllers
{
    [TestClass]
    public sealed class StripeAccountControllerTest
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
            ConstructorTests<StripeAccountController>.For(typeof(IMediator))
                .WithName("StripeAccountController")
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
        public async Task VerifyAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var address = FakeStripeFactory.CreateAddress();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<VerifyAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(CommandResult.Succeeded)
                .Verifiable();

            var controller = new StripeAccountController(_mockMediator.Object);

            // Act
            var result = await controller.VerifyAccountAsync(
                new VerifyAccountCommand(
                    address.Line1,
                    address.Line2,
                    address.City,
                    address.State,
                    address.PostalCode,
                    true
                )
            );

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<VerifyAccountCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }

        [TestMethod]
        public async Task VerifyAccountAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var address = FakeStripeFactory.CreateAddress();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<VerifyAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            var controller = new StripeAccountController(_mockMediator.Object);

            // Act
            var result = await controller.VerifyAccountAsync(
                new VerifyAccountCommand(
                    address.Line1,
                    address.Line2,
                    address.City,
                    address.State,
                    address.PostalCode,
                    true
                )
            );

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            _mockMediator.Verify(mock => mock.Send(It.IsAny<VerifyAccountCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
