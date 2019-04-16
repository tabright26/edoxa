// Filename: DeleteCardCommandHandlerTest.cs
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
    public sealed class DeleteCardCommandHandlerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public async Task HandleAsync_FindAsNoTrackingAsync_ShouldBeInvokedExactlyOneTime()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            var card = _userAggregateFactory.CreateCard();

            var mockUserRepository = new Mock<IUserRepository>();

            var mockCardService = new Mock<CardService>();

            mockUserRepository.Setup(repository => repository.FindAsNoTrackingAsync(It.IsAny<UserId>())).ReturnsAsync(user).Verifiable();

            mockCardService.Setup(
                               service => service.DeleteAsync(
                                   It.IsAny<string>(),
                                   It.IsAny<string>(),
                                   It.IsAny<RequestOptions>(),
                                   It.IsAny<CancellationToken>()
                               )
                           )
                           .ReturnsAsync(card)
                           .Verifiable();

            var handler = new DeleteCardCommandHandler(mockUserRepository.Object, mockCardService.Object);

            // Act
            await handler.HandleAsync(new DeleteCardCommand(user.Id, CardId.Parse(card.Id)));

            // Assert
            mockUserRepository.Verify(repository => repository.FindAsNoTrackingAsync(It.IsAny<UserId>()), Times.Once);

            mockCardService.Verify(
                service => service.DeleteAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<RequestOptions>(), It.IsAny<CancellationToken>()),
                Times.Once
            );
        }
    }
}