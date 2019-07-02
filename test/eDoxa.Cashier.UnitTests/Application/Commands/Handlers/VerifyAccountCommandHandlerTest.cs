// Filename: VerifyAccountCommandHandlerTest.cs
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
using eDoxa.Cashier.Api.Application.Commands.Handlers;
using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.UnitTests.Helpers.Mocks;
using eDoxa.Commands.Extensions;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Data.Fakers;
using eDoxa.Stripe.Models;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Application.Commands.Handlers
{
    [TestClass]
    public sealed class VerifyAccountCommandHandlerTest
    {
        private MockHttpContextAccessor _mockHttpContextAccessor;
        private Mock<IStripeService> _mockStripeService;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHttpContextAccessor = new MockHttpContextAccessor();
            _mockStripeService = new Mock<IStripeService>();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<VerifyAccountCommandHandler>.ForParameters(typeof(IStripeService), typeof(IHttpContextAccessor), typeof(IUserRepository))
                .WithClassName("VerifyAccountCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_VerifyAccountCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var addressFaker = new AddressFaker();

            var address = addressFaker.FakeAddress();

            var command = new VerifyAccountCommand(
                address.Line1,
                address.Line2,
                address.City,
                address.State,
                address.PostalCode,
                true
            );

            var userFaker = new UserFaker();

            var user = userFaker.FakeNewUser();

            _mockUserRepository.Setup(mock => mock.GetUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockStripeService.Setup(
                    mock => mock.VerifyAccountAsync(
                        It.IsAny<StripeConnectAccountId>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .Returns(Task.CompletedTask)
                .Verifiable();

            var handler = new VerifyAccountCommandHandler(_mockStripeService.Object, _mockHttpContextAccessor.Object, _mockUserRepository.Object);

            // Act
            await handler.HandleAsync(command);

            // Assert
            _mockStripeService.Verify(
                mock => mock.VerifyAccountAsync(
                    It.IsAny<StripeConnectAccountId>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<CancellationToken>()
                ),
                Times.Once
            );
        }
    }
}
