// Filename: UsersControllerTest.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Controllers;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;
using eDoxa.Challenges.DTO;
using eDoxa.Challenges.DTO.Queries;
using eDoxa.Functional.Maybe;
using eDoxa.Security.Services;
using eDoxa.Seedwork.Domain.Common.Enums;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Challenges.Api.Tests.Controllers
{
    [TestClass]
    public sealed class UsersControllerTest
    {
        private Mock<IUserInfoService> _userInfoService;
        private Mock<IMediator> _mediator;
        private Mock<IChallengeQueries> _queries;

        [TestInitialize]
        public void TestInitialize()
        {
            _userInfoService = new Mock<IUserInfoService>();
            _queries = new Mock<IChallengeQueries>();
            _mediator = new Mock<IMediator>();
        }

        [TestMethod]
        public async Task FindUserChallengeHistoryAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var value = new ChallengeListDTO
            {
                Items = new List<ChallengeDTO>
                {
                    new ChallengeDTO()
                }
            };

            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _queries.Setup(queries => queries.FindUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState1>()))
                .ReturnsAsync(new Option<ChallengeListDTO>(value))
                .Verifiable();

            var controller = new ChallengeHistoryController(_userInfoService.Object, _queries.Object);

            // Act
            var result = await controller.FindUserChallengeHistoryAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindUserChallengeHistoryAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _userInfoService.SetupGet(userInfoService => userInfoService.Subject).Returns(new Option<Guid>(Guid.NewGuid()));

            _queries.Setup(queries => queries.FindUserChallengeHistoryAsync(It.IsAny<UserId>(), It.IsAny<Game>(), It.IsAny<ChallengeType>(), It.IsAny<ChallengeState1>()))
                .ReturnsAsync(new Option<ChallengeListDTO>())
                .Verifiable();

            var controller = new ChallengeHistoryController(_userInfoService.Object, _queries.Object);

            // Act
            var result = await controller.FindUserChallengeHistoryAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }
    }
}