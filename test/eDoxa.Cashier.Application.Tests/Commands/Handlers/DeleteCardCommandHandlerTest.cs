﻿// Filename: DeleteCardCommandHandlerTest.cs
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
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class DeleteCardCommandHandlerTest
    {
        private static readonly FakeStripeFactory FakeStripeFactory = FakeStripeFactory.Instance;
        private Mock<IStripeService> _mockStripeService;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.SetupMethods();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DeleteCardCommandHandler>.For(typeof(IUserInfoService), typeof(IStripeService))
                .WithName("DeleteCardCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_DeleteCardCommand_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var card = FakeStripeFactory.CreateCard();

            var handler = new DeleteCardCommandHandler(_mockUserInfoService.Object, _mockStripeService.Object);

            // Act
            await handler.HandleAsync(new DeleteCardCommand(new StripeCardId(card.Id)));

            // Assert
            _mockStripeService.Verify(mock => mock.DeleteCardAsync(It.IsAny<StripeCustomerId>(), It.IsAny<StripeCardId>(), It.IsAny<CancellationToken>()),
                Times.Once);
        }
    }
}