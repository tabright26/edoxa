// Filename: UpdateEmailCommandHandlerTest.cs
// Date Created: 2019-05-03
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
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Commands.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class UpdateEmailCommandHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task HandleAsync_FindAsNoTrackingAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var customer = _userAggregateFactory.CreateCustomer();

            var mockCustomerService = new Mock<CustomerService>();

            mockCustomerService.Setup(
                    service => service.UpdateAsync(
                        It.IsAny<string>(),
                        It.IsAny<CustomerUpdateOptions>(),
                        It.IsAny<RequestOptions>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(customer)
                .Verifiable();

            var handler = new UpdateEmailCommandHandler(mockCustomerService.Object);

            // Act
            await handler.HandleAsync(new UpdateEmailCommand(CustomerId.Parse(customer.Id), customer.Email));

            // Assert
            mockCustomerService.Verify(
                service =>
                    service.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}