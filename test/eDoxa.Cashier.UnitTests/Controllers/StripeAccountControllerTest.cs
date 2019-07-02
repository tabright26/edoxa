// Filename: StripeAccountControllerTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Data.Fakers;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Controllers
{
    [TestClass]
    public sealed class StripeAccountControllerTest
    {
        private Mock<IMediator> _mockMediator;
        private Mock<IUserQuery> _mockUserQuery;
        private Mock<IStripeService> _mockStripeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
            _mockUserQuery = new Mock<IUserQuery>();
            _mockStripeService = new Mock<IStripeService>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<StripeAccountController>.ForParameters(typeof(IMediator), typeof(IUserQuery), typeof(IStripeService))
                .WithClassName("StripeAccountController")
                .WithClassAttributes(
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
            var addressFaker = new AddressFaker();

            var address = addressFaker.FakeAddress();

            _mockMediator.Setup(mock => mock.Send(It.IsAny<VerifyAccountCommand>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var controller = new StripeAccountController(_mockMediator.Object, _mockUserQuery.Object, _mockStripeService.Object);

            // Act
            var result = await controller.PatchAsync(
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
    }
}
