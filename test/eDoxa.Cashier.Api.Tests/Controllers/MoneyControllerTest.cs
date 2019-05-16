// Filename: MoneyControllerTest.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.DTO;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class MoneyControllerTest
    {
        private Mock<IMediator> _mockMediator;
        private Mock<IMoneyAccountQueries> _mockMoneyAccountQueries;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
            _mockMoneyAccountQueries = new Mock<IMoneyAccountQueries>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<MoneyController>.For(typeof(IUserInfoService), typeof(IMoneyAccountQueries), typeof(IMediator))
                .WithName("MoneyController")
                .WithAttributes(typeof(AuthorizeAttribute), typeof(ApiControllerAttribute), typeof(ApiVersionAttribute), typeof(ProducesAttribute),
                    typeof(RouteAttribute))
                .Assert();
        }

        [TestMethod]
        public async Task GetMoneyAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockMoneyAccountQueries.Setup(queries => queries.GetMoneyAccountAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new Option<MoneyAccountDTO>(new MoneyAccountDTO()))
                .Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetMoneyAccountAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetMoneyAccountAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            _mockMoneyAccountQueries.Setup(queries => queries.GetMoneyAccountAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<MoneyAccountDTO>())
                .Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetMoneyAccountAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task AddFundsAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var command = new AddFundsCommand(MoneyBundleType.Ten);

            _mockMediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(Money.OneHundred)).Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.AddFundsAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.Verify();
        }

        [TestMethod]
        public void GetMoneyBundles_ShouldBeEquivalentToOkObjectResult()
        {
            // Arrange
            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = controller.GetMoneyBundles();

            // Assert
            result.Should().BeEquivalentTo(new OkObjectResult(MoneyBundleType.GetAll()));
        }

        [TestMethod]
        public async Task GetMoneyTransactionsAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockMoneyAccountQueries.Setup(queries => queries.GetMoneyTransactionsAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<MoneyTransactionListDTO>(new MoneyTransactionListDTO
            {
                Items = new List<MoneyTransactionDTO>
                {
                    new MoneyTransactionDTO()
                }
            })).Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetMoneyTransactionsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetMoneyTransactionsAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            _mockMoneyAccountQueries.Setup(queries => queries.GetMoneyTransactionsAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<MoneyTransactionListDTO>()).Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetMoneyTransactionsAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task WithdrawalFundsAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var command = new WithdrawalFundsCommand(WithdrawalMoneyBundleType.Fifty);

            _mockMediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(Money.OneHundred)).Verifiable();

            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.WithdrawalFundsAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockMoneyAccountQueries.Verify();

            _mockMediator.Verify();
        }

        [TestMethod]
        public void GetWithdrawalMoneyBundles_ShouldBeEquivalentToOkObjectResult()
        {
            // Arrange
            var controller = new MoneyController(_mockUserInfoService.Object, _mockMoneyAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = controller.GetWithdrawalMoneyBundles();

            // Assert
            result.Should().BeEquivalentTo(new OkObjectResult(WithdrawalMoneyBundleType.GetAll()));
        }
    }
}