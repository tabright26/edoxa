// Filename: CreateCardCommandHandlerTest.cs
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
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using Stripe;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateCardCommandHandlerTest
    {
        private static readonly UserAggregateFactory UserAggregateFactory = UserAggregateFactory.Instance;
        private Mock<CardService> _mockCardService;
        private Mock<CustomerService> _mockCustomerService;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockCardService = new Mock<CardService>();
            _mockCustomerService = new Mock<CustomerService>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public async Task Handle_FindAsNoTrackingAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var customer = UserAggregateFactory.CreateCustomer();

            var card = UserAggregateFactory.CreateCard();

            _mockCustomerService.Setup(
                    service => service.UpdateAsync(
                        It.IsAny<string>(),
                        It.IsAny<CustomerUpdateOptions>(),
                        It.IsAny<RequestOptions>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(customer)
                .Verifiable();

            _mockCardService.Setup(
                    service => service.CreateAsync(
                        It.IsAny<string>(),
                        It.IsAny<CardCreateOptions>(),
                        It.IsAny<RequestOptions>(),
                        It.IsAny<CancellationToken>()
                    )
                )
                .ReturnsAsync(card)
                .Verifiable();

            var handler = new CreateCardCommandHandler(_mockUserInfoService.Object, _mockCustomerService.Object, _mockCardService.Object);

            // Act
            var response = await handler.Handle(new CreateCardCommand(card.Id, true), default);

            // Assert
            response.Should().BeEquivalentTo(new OkObjectResult(card));

            _mockCustomerService.Verify(
                service =>
                    service.UpdateAsync(It.IsAny<string>(), It.IsAny<CustomerUpdateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );

            _mockCardService.Verify(
                service => service.CreateAsync(It.IsAny<string>(), It.IsAny<CardCreateOptions>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}