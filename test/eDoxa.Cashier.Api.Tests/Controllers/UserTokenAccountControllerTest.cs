// Filename: UserTokenAccountControllerTest.cs
// Date Created: 2019-04-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.DTO.Queries;
using eDoxa.Functional.Maybe;
using eDoxa.Security.Services;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UserTokenAccountControllerTest
    {
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        private Mock<IUserInfoService> _userInfoService;
        private Mock<IMediator> _mediator;
        private Mock<ITokenAccountQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _userInfoService = new Mock<IUserInfoService>();
            _queries = new Mock<ITokenAccountQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task BuyTokensAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var token = _userAggregateFactory.CreateToken();

            var command = new DepositTokensCommand(TokenBundleType.FiftyThousand);

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _mediator.Setup(mediator => mediator.Send(command, default)).ReturnsAsync(new OkObjectResult(token)).Verifiable();

            var controller = new AccountTokenController(_userInfoService.Object, _queries.Object, _mediator.Object);

            // Act
            var result = await controller.DepositTokensAsync(command);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.Verify();
        }
    }
}