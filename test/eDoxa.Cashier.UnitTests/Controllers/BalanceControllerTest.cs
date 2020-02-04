// Filename: BalanceControllerTest.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

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
        public async Task FindUserBalanceAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            TestMock.AccountQuery.Setup(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<UserId>(), It.IsAny<CurrencyType>()))
                .ReturnsAsync((Balance) null);

            var controller = new BalanceController(TestMock.AccountQuery.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await controller.FindUserBalanceAsync(CurrencyType.Money);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            TestMock.AccountQuery.Verify(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<UserId>(), It.IsAny<CurrencyType>()), Times.Once);
        }

        [Fact]
        public async Task FindUserBalanceAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var account = new Account(new UserId());

            TestMock.AccountQuery.Setup(mediator => mediator.FindUserBalanceAsync(It.IsAny<UserId>(), It.IsAny<CurrencyType>()))
                .ReturnsAsync(account.GetBalanceFor(CurrencyType.Money))
                .Verifiable();

            var controller = new BalanceController(TestMock.AccountQuery.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await controller.FindUserBalanceAsync(CurrencyType.Money);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            TestMock.AccountQuery.Verify(accountQuery => accountQuery.FindUserBalanceAsync(It.IsAny<UserId>(), It.IsAny<CurrencyType>()), Times.Once);
        }
    }
}
