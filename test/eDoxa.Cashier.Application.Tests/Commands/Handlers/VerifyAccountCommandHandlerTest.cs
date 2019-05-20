﻿// Filename: VerifyAccountCommandHandlerTest.cs
// Date Created: 2019-05-19
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
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Extensions;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class VerifyAccountCommandHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<ICashierHttpContext> _mockCashierHttpContext;
        private Mock<IStripeService> _mockStripeService;
        private Mock<IUserRepository> _mockUserRepository;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockCashierHttpContext = new Mock<ICashierHttpContext>();
            _mockCashierHttpContext.SetupGetProperties();
            _mockUserRepository = new Mock<IUserRepository>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<VerifyAccountCommandHandler>.For(typeof(IStripeService), typeof(ICashierHttpContext), typeof(IUserRepository))
                                                         .WithName("VerifyAccountCommandHandler")
                                                         .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_VerifyAccountCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var address = FakeStripeFactory.CreateAddress();

            var command = new VerifyAccountCommand(
                address.Line1,
                address.Line2,
                address.City,
                address.State,
                address.PostalCode,
                true
            );

            var user = FakeCashierFactory.CreateUser();

            _mockUserRepository.Setup(mock => mock.FindUserAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            _mockStripeService.Setup(
                                  mock => mock.VerifyAccountAsync(
                                      It.IsAny<StripeAccountId>(),
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

            var handler = new VerifyAccountCommandHandler(_mockStripeService.Object, _mockCashierHttpContext.Object, _mockUserRepository.Object);

            // Act
            var result = await handler.HandleAsync(command);

            // Assert
            result.Should().BeOfType<Either<ValidationError, CommandResult>>();

            _mockStripeService.Verify(
                mock => mock.VerifyAccountAsync(
                    It.IsAny<StripeAccountId>(),
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
