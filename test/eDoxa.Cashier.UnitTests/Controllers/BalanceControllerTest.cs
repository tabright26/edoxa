// Filename: BalanceControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Controllers
{
    public sealed class BalanceControllerTest : UnitTest
    {
        public BalanceControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task GetByCurrencyAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockAccountQuery = new Mock<IAccountQuery>();

            var controller = new BalanceController(mockAccountQuery.Object, TestMapper);

            // Act
            var result = await controller.GetByCurrencyAsync(Currency.Money);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<UserId>(), It.IsAny<Currency>()), Times.Once);
        }

        [Fact]
        public async Task GetByCurrencyAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockAccountQuery = new Mock<IAccountQuery>();

            var account = new Account(new UserId());

            mockAccountQuery.Setup(mediator => mediator.FindUserBalanceAsync(It.IsAny<UserId>(), It.IsAny<Currency>()))
                .ReturnsAsync(account.GetBalanceFor(Currency.Money))
                .Verifiable();

            var controller = new BalanceController(mockAccountQuery.Object, TestMapper);

            // Act
            var result = await controller.GetByCurrencyAsync(Currency.Money);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountQuery.Verify(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<UserId>(), It.IsAny<Currency>()), Times.Once);
        }
    }
}
