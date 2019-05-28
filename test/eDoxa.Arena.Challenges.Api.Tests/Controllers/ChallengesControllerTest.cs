// Filename: ChallengesControllerTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Controllers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.DTO;
using eDoxa.Arena.Challenges.DTO.Queries;
using eDoxa.Arena.Challenges.Services.Abstractions;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Enumerations;

using FluentAssertions;

using MediatR;

using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.Api.Tests.Controllers
{
    [TestClass]
    public sealed class ChallengesControllerTest
    {
        private Mock<IMediator> _mediator;
        private Mock<IMapper> _mapper;
        private Mock<IChallengeQuery> _queries;
        private Mock<IChallengeService> _challengeService;

        [TestInitialize]
        public void TestInitialize()
        {
            _queries = new Mock<IChallengeQuery>();
            _mapper = new Mock<IMapper>();
            _mediator = new Mock<IMediator>();
            _challengeService = new Mock<IChallengeService>();
        }

        [TestMethod]
        public async Task FindChallengesAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var value = new ChallengeListDTO
            {
                Items = new List<ChallengeDTO>
                {
                    new ChallengeDTO()
                }
            };

            _queries.Setup(queries => queries.GetChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>())).ReturnsAsync(new Option<ChallengeListDTO>(value)).Verifiable();

            var controller = new ChallengesController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.GetChallengesAsync(null, null);

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengesAsync_ShouldBeNoContentResult()
        {
            // Arrange
            _queries.Setup(queries => queries.GetChallengesAsync(It.IsAny<Game>(), It.IsAny<ChallengeState>())).ReturnsAsync(new Option<ChallengeListDTO>()).Verifiable();

            var controller = new ChallengesController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.GetChallengesAsync(null, null);

            // Assert
            result.Should().BeOfType<NoContentResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengeAsync_ShouldBeOkObjectResult()
        {
            // Arrange        
            _queries.Setup(queries => queries.GetChallengeAsync(It.IsAny<ChallengeId>()))
                    .ReturnsAsync(new Option<ChallengeDTO>(new ChallengeDTO()))
                    .Verifiable();

            var controller = new ChallengesController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.GetChallengeAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }

        [TestMethod]
        public async Task FindChallengeAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            _queries.Setup(queries => queries.GetChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(new Option<ChallengeDTO>()).Verifiable();

            var controller = new ChallengesController(_queries.Object, _mediator.Object);

            // Act
            var result = await controller.GetChallengeAsync(It.IsAny<ChallengeId>());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            _queries.Verify();

            _mediator.VerifyNoOtherCalls();
        }
    }
}
