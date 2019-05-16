// Filename: AccountControllerTest.cs
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
    public sealed class AccountControllerTest
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
            ConstructorTests<AccountController>.For(typeof(IMediator))
                .WithName("AccountController")
                .WithAttributes(typeof(AuthorizeAttribute), typeof(ApiControllerAttribute), typeof(ApiVersionAttribute), typeof(ProducesAttribute),
                    typeof(RouteAttribute))
                .Assert();
        }

        [TestMethod]
        public async Task VerifyAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var account = FakeStripeFactory.CreateAccount();

            var command = new VerifyAccountCommand(account.Individual.Address.Line1, account.Individual.Address.City, account.Individual.Address.State,
                account.Individual.Address.PostalCode, true);

            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<VerifyAccountCommand>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new OkObjectResult(string.Empty)).Verifiable();

            var controller = new AccountController(_mockMediator.Object);

            // Act
            var result = await controller.VerifyAccountAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<VerifyAccountCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}