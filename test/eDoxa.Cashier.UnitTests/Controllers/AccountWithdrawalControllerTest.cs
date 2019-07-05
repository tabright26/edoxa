// Filename: AccountWithdrawalControllerTest.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Seedwork.Testing.TestConstructor;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Controllers
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
        public void Constructor_Tests()
        {
            TestConstructor<AccountWithdrawalController>.ForParameters(typeof(IMediator))
                .WithClassName("AccountWithdrawalController")
                .WithClassAttributes(
                    typeof(AuthorizeAttribute),
                    typeof(ApiControllerAttribute),
                    typeof(ApiVersionAttribute),
                    typeof(ProducesAttribute),
                    typeof(RouteAttribute),
                    typeof(ApiExplorerSettingsAttribute)
                )
                .Assert();
        }

        [TestMethod]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockMediator.Setup(mediator => mediator.Send(It.IsAny<WithdrawalCommand>(), It.IsAny<CancellationToken>())).Returns(Unit.Task).Verifiable();

            var controller = new AccountWithdrawalController(_mockMediator.Object);

            // Act
            var result = await controller.PostAsync(new WithdrawalCommand(Money.Fifty));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMediator.Verify(mediator => mediator.Send(It.IsAny<WithdrawalCommand>(), It.IsAny<CancellationToken>()), Times.Once);
        }
    }
}
