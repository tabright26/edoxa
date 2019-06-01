﻿// Filename: VerifyAccountCommandHandlerTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Tests.Utilities.Fakes;
using eDoxa.Cashier.Tests.Utilities.Mocks.Extensions;
using eDoxa.Seedwork.Application.Commands.Extensions;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Stripe.Abstractions;
using eDoxa.Stripe.Models;
using eDoxa.Stripe.Tests.Utilities;
using eDoxa.Testing.MSTest;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class VerifyAccountCommandHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private static readonly StripeBuilder StripeBuilder = StripeBuilder.Instance;
        private Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private Mock<IStripeService> _mockStripeService;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockHttpContextAccessor.SetupClaims();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<VerifyAccountCommandHandler>.For(typeof(IStripeService), typeof(IHttpContextAccessor), typeof(IUserRepository))
                .WithName("VerifyAccountCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_VerifyAccountCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var address = StripeBuilder.CreateAddress();

            var command = new VerifyAccountCommand(
                address.Line1,
                address.Line2,
                address.City,
                address.State,
                address.PostalCode,
                true
            );

            var user = FakeCashierFactory.CreateUser();

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
