// Filename: UpdateEmailCommandHandlerTest.cs
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
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class UpdateEmailCommandHandlerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
        private Mock<IStripeService> _mockStripeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockStripeService = new Mock<IStripeService>();
            _mockStripeService.SetupMethods();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<UpdateEmailCommandHandler>.For(typeof(IStripeService))
                .WithName("UpdateEmailCommandHandler")
                .Assert();
        }

        [TestMethod]
        public async Task HandleAsync_UpdateEmailCommand_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var customer = FakeCashierFactory.CreateCustomer();

            //mockCustomerService.Setup(
            //        service => service.UpdateAsync(
            //            It.IsAny<string>(),
            //            It.IsAny<CustomerUpdateOptions>(),
            //            It.IsAny<RequestOptions>(),
            //            It.IsAny<CancellationToken>()
            //        )
            //    )
            //    .ReturnsAsync(customer)
            //    .Verifiable();

            var handler = new UpdateEmailCommandHandler(_mockStripeService.Object);

            // Act
            await handler.HandleAsync(new UpdateEmailCommand(CustomerId.Parse(customer.Id), customer.Email));

            // Assert
            _mockStripeService.Verify(mock => mock.UpdateCustomerEmailAsync(It.IsAny<CustomerId>(), It.IsAny<string>(), It.IsAny<CancellationToken>()),
                Times.Once);

            //mockCustomerService.Verify(
            //    service =>
            //        service.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
            //    Times.Once
            //);
        }
    }
}