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
using eDoxa.Cashier.Tests.Extensions;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Commands.Extensions;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class UpdateCardDefaultCommandHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
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
            ConstructorTests<UpdateCardDefaultCommandHandler>.For(typeof(IUserInfoService), typeof(IStripeService))
                .WithName("UpdateCardDefaultCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_UpdateCardDefaultCommand_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var cardId = FakeCashierFactory.CreateCardId();

            //_mockCustomerService.Setup(
            //        service => service.UpdateAsync(
            //            It.IsAny<string>(),
            //            It.IsAny<CustomerUpdateOptions>(),
            //            It.IsAny<RequestOptions>(),
            //            It.IsAny<CancellationToken>()
            //        )
            //    )
            //    .ReturnsAsync(customer)
            //    .Verifiable();

            var handler = new UpdateCardDefaultCommandHandler(_mockUserInfoService.Object, _mockStripeService.Object);

            // Act
            var result = await handler.HandleAsync(new UpdateCardDefaultCommand(cardId));

            // Assert
            result.Should().BeEquivalentTo(new OkResult());

            _mockStripeService.Verify(mock => mock.UpdateCustomerDefaultSourceAsync(It.IsAny<CustomerId>(), It.IsAny<CardId>(), It.IsAny<CancellationToken>()),
                Times.Once);

            //_mockCustomerService.Verify(
            //    service =>
            //        service.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
            //    Times.Once
            //);
        }
    }
}