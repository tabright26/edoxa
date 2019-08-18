// Filename: AccountWithdrawalControllerTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Cashier.Api.Areas.Accounts.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Areas.Account.Controllers
{
    [TestClass]
    public sealed class AccountWithdrawalControllerTest
    {
        private Mock<IMediator> _mockMediator;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<WithdrawalRequest>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var controller = new AccountWithdrawalController(_mockMediator.Object);

            // Act
            var result = await controller.PostAsync(new WithdrawalRequest(Money.Fifty));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<WithdrawalRequest>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
