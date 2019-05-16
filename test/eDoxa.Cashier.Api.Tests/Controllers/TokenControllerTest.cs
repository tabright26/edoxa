// Filename: TokenControllerTest.cs
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
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
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
    public sealed class TokenControllerTest
    {
        private Mock<IMediator> _mockMediator;
        private Mock<ITokenAccountQueries> _mockTokenAccountQueries;
        private Mock<IUserInfoService> _mockUserInfoService;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockMediator = new Mock<IMediator>();
            _mockTokenAccountQueries = new Mock<ITokenAccountQueries>();
            _mockUserInfoService = new Mock<IUserInfoService>();
            _mockUserInfoService.SetupGetProperties();
        }

        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<TokenController>.For(typeof(IUserInfoService), typeof(ITokenAccountQueries), typeof(IMediator))
                .WithName("TokenController")
                .WithAttributes(typeof(AuthorizeAttribute), typeof(ApiControllerAttribute), typeof(ApiVersionAttribute), typeof(ProducesAttribute),
                    typeof(RouteAttribute))
                .Assert();
        }

        [TestMethod]
        public async Task GetMoneyAccountAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockTokenAccountQueries.Setup(queries => queries.GetTokenAccountAsync(It.IsAny<UserId>()))
                .ReturnsAsync(new Option<TokenAccountDTO>(new TokenAccountDTO()))
                .Verifiable();

            var controller = new TokenController(_mockUserInfoService.Object, _mockTokenAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetTokenAccountAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockTokenAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetMoneyAccountAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            _mockTokenAccountQueries.Setup(queries => queries.GetTokenAccountAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<TokenAccountDTO>())
                .Verifiable();

            var controller = new TokenController(_mockUserInfoService.Object, _mockTokenAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetTokenAccountAsync();

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _mockTokenAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetMoneyTransactionsAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            _mockTokenAccountQueries.Setup(queries => queries.GetTokenTransactionsAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<TokenTransactionListDTO>(new TokenTransactionListDTO
            {
                Items = new List<TokenTransactionDTO>
                {
                    new TokenTransactionDTO()
                }
            })).Verifiable();

            var controller = new TokenController(_mockUserInfoService.Object, _mockTokenAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetTokenTransactionsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockTokenAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task GetMoneyTransactionsAsync_ShouldBeOfTypeNoContentResult()
        {
            // Arrange
            _mockTokenAccountQueries.Setup(queries => queries.GetTokenTransactionsAsync(It.IsAny<UserId>())).ReturnsAsync(new Option<TokenTransactionListDTO>()).Verifiable();

            var controller = new TokenController(_mockUserInfoService.Object, _mockTokenAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.GetTokenTransactionsAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _mockTokenAccountQueries.Verify();

            _mockMediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task BuyTokensAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var command = new BuyTokensCommand(TokenBundleType.FiftyThousand);

            _mockMediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(Token.OneHundredThousand)).Verifiable();

            var controller = new TokenController(_mockUserInfoService.Object, _mockTokenAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.BuyTokensAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockTokenAccountQueries.Verify();

            _mockMediator.Verify();
        }

        [TestMethod]
        public void GetTokenBundlesAsync_ShouldBeEquivalentToOkObjectResult()
        {
            // Arrange
            var controller = new TokenController(_mockUserInfoService.Object, _mockTokenAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = controller.GetTokenBundlesAsync();

            // Assert
            result.Should().BeEquivalentTo(new OkObjectResult(TokenBundleType.GetAll()));
        }
    }
}