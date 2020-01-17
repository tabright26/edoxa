// Filename: AccountDepositControllerTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Services;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;

using FluentAssertions;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Controllers
{
    public sealed class AccountDepositControllerTest : UnitTest
    {
  

        [Fact]
        public async Task GetAsync_WithCurrencyAll_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService
                .Setup(accountService => accountService.FetchTransactionBundlesAsync(It.IsAny<EnumTransactionType>(), It.IsAny<EnumCurrency>(), true))
                .ReturnsAsync(Array.Empty<TransactionBundleDto>())
                .Verifiable();

            var controller = new TransactionBundlesController(mockAccountService.Object);

            // Act
            var result = await controller.GetAsync(EnumTransactionType.Charge, EnumCurrency.Money);

            // Assert
            result.Should().BeOfType<NotFoundResult>();

            mockAccountService.Verify(
                accountService => accountService.FetchTransactionBundlesAsync(It.IsAny<EnumTransactionType>(), It.IsAny<EnumCurrency>(), true),
                Times.Once);
        }

        [Fact]
        public async Task GetAsync_WithCurrencyMoney_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService
                .Setup(accountService => accountService.FetchTransactionBundlesAsync(It.IsAny<EnumTransactionType>(), It.IsAny<EnumCurrency>(), true))
                .ReturnsAsync(
                    new[]
                    {
                        new TransactionBundleDto
                        {
                            Id = 1,
                            Type = EnumTransactionType.Deposit,
                            Currency = new CurrencyDto
                            {
                                Type = EnumCurrency.Money,
                                Amount = Convert.ToDouble(Money.OneHundred.Amount)
                            },
                            Price = new CurrencyDto
                            {
                                Type = EnumCurrency.Money,
                                Amount = Convert.ToDouble(Money.OneHundred.Amount)
                            },
                            Description = null,
                            Notes = null,
                            Deprecated = false,
                            Disabled = false
                        }
                    })
                .Verifiable();

            var controller = new TransactionBundlesController(mockAccountService.Object);

            // Act
            var result = await controller.GetAsync(EnumTransactionType.Deposit, EnumCurrency.Money);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountService.Verify(
                accountService => accountService.FetchTransactionBundlesAsync(It.IsAny<EnumTransactionType>(), It.IsAny<EnumCurrency>(), true),
                Times.Once);
        }

        [Fact]
        public async Task GetAsync_WithCurrencyToken_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockAccountService = new Mock<IAccountService>();

            mockAccountService
                .Setup(accountService => accountService.FetchTransactionBundlesAsync(It.IsAny<EnumTransactionType>(), It.IsAny<EnumCurrency>(), true))
                .ReturnsAsync(
                    new[]
                    {
                        new TransactionBundleDto
                        {
                            Id = 1,
                            Type = EnumTransactionType.Deposit,
                            Currency = new CurrencyDto
                            {
                                Type = EnumCurrency.Token,
                                Amount = Convert.ToDouble(Token.OneMillion.Amount)
                            },
                            Price = new CurrencyDto
                            {
                                Type = EnumCurrency.Money,
                                Amount = Convert.ToDouble(Money.OneHundred.Amount)
                            },
                            Description = null,
                            Notes = null,
                            Deprecated = false,
                            Disabled = false
                        }
                    })
                .Verifiable();

            var controller = new TransactionBundlesController(mockAccountService.Object);

            // Act
            var result = await controller.GetAsync(EnumTransactionType.Deposit, EnumCurrency.Token);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockAccountService.Verify(
                accountService => accountService.FetchTransactionBundlesAsync(It.IsAny<EnumTransactionType>(), It.IsAny<EnumCurrency>(), true),
                Times.Once);
        }

        public AccountDepositControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }
    }
}
