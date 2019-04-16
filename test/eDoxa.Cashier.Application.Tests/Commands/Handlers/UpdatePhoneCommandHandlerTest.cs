// Filename: UpdatePhoneCommandHandlerTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Repositories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class UpdatePhoneCommandHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task HandleAsync_FindAsNoTrackingAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var customer = _userAggregateFactory.CreateCustomer();

            var mockUserRepository = new Mock<IUserRepository>();

            var mockCustomerService = new Mock<CustomerService>();

            mockUserRepository.Setup(repository => repository.FindAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            mockCustomerService.Setup(service => service.GetAsync(It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()))
                               .ReturnsAsync(customer)
                               .Verifiable();

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

            var handler = new UpdatePhoneCommandHandler(mockUserRepository.Object, mockCustomerService.Object);

            // Act
            await handler.HandleAsync(new UpdatePhoneCommand(user.Id, customer.Shipping.Phone));

            // Assert
            mockUserRepository.Verify(repository => repository.FindAsNoTrackingAsync(It.IsAny<UserId>()), Times.Once);

            mockCustomerService.Verify(service => service.GetAsync(It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()), Times.Once);

            mockCustomerService.Verify(
                service =>
                    service.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}