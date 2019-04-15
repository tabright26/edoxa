// Filename: CreateCardCommandHandlerTest.cs
// Date Created: 2019-04-14
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

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateCardCommandHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task Handle_FindAsNoTrackingAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var customer = _userAggregateFactory.CreateCustomer();

            var card = _userAggregateFactory.CreateCard();

            var mockUserRepository = new Mock<IUserRepository>();

            var mockCustomerService = new Mock<CustomerService>();

            mockUserRepository.Setup(repository => repository.FindAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

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

            var mockCardService = new Mock<CardService>();

            mockCardService.Setup(
                               service => service.CreateAsync(
                                   It.IsAny<string>(),
                                   It.IsAny<CardCreateOptions>(),
                                   It.IsAny<RequestOptions>(),
                                   It.IsAny<CancellationToken>()
                               )
                           )
                           .ReturnsAsync(card)
                           .Verifiable();

            var handler = new CreateCardCommandHandler(mockUserRepository.Object, mockCustomerService.Object, mockCardService.Object);

            // Act
            var response = await handler.Handle(new CreateCardCommand(card.Id, true), default);

            // Assert
            response.Should().Be(card);

            mockUserRepository.Verify(repository => repository.FindAsNoTrackingAsync(It.IsAny<UserId>()), Times.Once);

            mockCustomerService.Verify(
                service =>
                    service.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );

            mockCardService.Verify(
                service => service.CreateAsync(It.IsAny<string>(), It.IsAny<CardCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}