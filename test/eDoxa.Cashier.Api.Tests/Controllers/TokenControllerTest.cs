// Filename: TokenControllerTest.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Cashier.Tests.Factories;
using eDoxa.Security.Abstractions;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class TokenControllerTest
    {
        private static readonly FakeCashierFactory FakeCashierFactory = FakeCashierFactory.Instance;
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
        public async Task DepositTokensAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var token = FakeCashierFactory.CreateToken();

            var command = new BuyTokensCommand(TokenBundleType.FiftyThousand);

            _mockMediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(token)).Verifiable();

            var controller = new TokenController(_mockUserInfoService.Object, _mockTokenAccountQueries.Object, _mockMediator.Object);

            // Act
            var result = await controller.BuyTokensAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _mockTokenAccountQueries.Verify();

            _mockMediator.Verify();
        }
    }
}