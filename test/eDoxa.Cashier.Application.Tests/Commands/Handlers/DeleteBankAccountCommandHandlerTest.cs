// Filename: DeleteBankAccountCommandHandlerTest.cs
// Date Created: 2019-05-13
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
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Cashier.Security.Abstractions;
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Commands.Extensions;
using eDoxa.Commands.Result;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Validations;
using eDoxa.ServiceBus;
using eDoxa.Testing.MSTest;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DeleteBankAccountCommandHandlerTest
    {
        private Mock<ICashierHttpContext> _mockCashierHttpContext;
        private Mock<IIntegrationEventService> _mockIntegrationEventService;
        private Mock<IStripeService> _mockStripeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.SetupMethods();
            _mockCashierHttpContext = new Mock<ICashierHttpContext>();
            _mockCashierHttpContext.SetupGetProperties();
            _mockIntegrationEventService = new Mock<IIntegrationEventService>();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DeleteBankAccountCommandHandler>.For(typeof(ICashierHttpContext), typeof(IStripeService), typeof(IIntegrationEventService))
                                                             .WithName("DeleteBankAccountCommandHandler")
                                                             .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DeleteBankAccountCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var handler = new DeleteBankAccountCommandHandler(_mockCashierHttpContext.Object, _mockStripeService.Object, _mockIntegrationEventService.Object);

            // Act
            var result = await handler.HandleAsync(new DeleteBankAccountCommand());

            // Assert
            result.Should().BeOfType<Either<ValidationError, CommandResult>>();

            _mockStripeService.Verify(
                mock => mock.DeleteBankAccountAsync(It.IsAny<StripeAccountId>(), It.IsAny<StripeBankAccountId>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
