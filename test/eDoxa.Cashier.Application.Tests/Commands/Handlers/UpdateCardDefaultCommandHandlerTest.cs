// Filename: UpdateCardDefaultCommandHandlerTest.cs
// Date Created: 2019-05-06
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
    public sealed class UpdateCardDefaultCommandHandlerTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<ICashierHttpContext> _mockCashierHttpContext;
        private Mock<IStripeService> _mockStripeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.SetupMethods();
            _mockCashierHttpContext = new Mock<ICashierHttpContext>();
            _mockCashierHttpContext.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<UpdateCardDefaultCommandHandler>.For(typeof(ICashierHttpContext), typeof(IStripeService))
                                                             .WithName("UpdateCardDefaultCommandHandler")
                                                             .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_UpdateCardDefaultCommand_ShouldBeOfTypeEither()
        {
            // Arrange
            var cardId = FakeStripeFactory.CreateCardId();

            var handler = new UpdateCardDefaultCommandHandler(_mockCashierHttpContext.Object, _mockStripeService.Object);

            // Act
            var result = await handler.HandleAsync(new UpdateCardDefaultCommand(cardId));

            // Assert
            result.Should().BeOfType<Either<ValidationError, CommandResult>>();

            _mockStripeService.Verify(
                mock => mock.UpdateCardDefaultAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}
