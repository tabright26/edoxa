// Filename: ChallengesControllerTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Challenges.Controllers;
using eDoxa.Cashier.Api.Areas.Challenges.Services.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Requests;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Application.FluentValidation.Extensions;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using FluentValidation.Results;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Challenges.Controllers
{
    public sealed class ChallengesControllerTest : UnitTest
    {
        public ChallengesControllerTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeNoContentObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync()).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FetchChallengesAsync(), Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();

            var challengeService = new Mock<IChallengeService>();

            var entreFee = new EntryFee(5000, Currency.Token);

            var bucket = new Bucket(Prize.None, BucketSize.Individual);

            var buckets = new Buckets(
                new List<Bucket>
                {
                    bucket
                });

            var payoutBuckets = new Payout(buckets);

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FetchChallengesAsync())
                .ReturnsAsync(
                    new List<Challenge>
                    {
                        new Challenge(new ChallengeId(), entreFee, payoutBuckets),
                        new Challenge(new ChallengeId(), entreFee, payoutBuckets),
                        new Challenge(new ChallengeId(), entreFee, payoutBuckets)
                    })
                .Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, challengeService.Object);

            // Act
            var result = await controller.GetAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FetchChallengesAsync(), Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeNotFoundObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            var challengeService = new Mock<IChallengeService>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, challengeService.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var challenge = TestData.FakerFactory.CreateChallengeFaker(1000).FakeChallenge();

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            var challengeService = new Mock<IChallengeService>();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, challengeService.Object);

            // Act
            var result = await controller.GetByIdAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeBadRequestObjectResult()
        {
            // Arrange
            var mockChallengeQuery = new Mock<IChallengeQuery>();
            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService
                .Setup(
                    challengeService => challengeService.CreateChallengeAsync(
                        It.IsAny<ChallengeId>(),
                        It.IsAny<PayoutEntries>(),
                        It.IsAny<EntryFee>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationFailure("test", "test error").ToResult())
                .Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.PostAsync(
                new CreateChallengeRequest(
                    new Guid(),
                    10,
                    5000,
                    "Token"));

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            mockChallengeService.Verify(
                challengeService => challengeService.CreateChallengeAsync(
                    It.IsAny<ChallengeId>(),
                    It.IsAny<PayoutEntries>(),
                    It.IsAny<EntryFee>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);
        }

        [Fact]
        public async Task PostAsync_ShouldBeOfTypeOkObjectResult()
        {
            // Arrange
            var challenge = TestData.FakerFactory.CreateChallengeFaker(1000).FakeChallenge();

            var mockChallengeQuery = new Mock<IChallengeQuery>();

            var mockChallengeService = new Mock<IChallengeService>();

            mockChallengeService
                .Setup(
                    challengeService => challengeService.CreateChallengeAsync(
                        It.IsAny<ChallengeId>(),
                        It.IsAny<PayoutEntries>(),
                        It.IsAny<EntryFee>(),
                        It.IsAny<CancellationToken>()))
                .ReturnsAsync(new ValidationResult())
                .Verifiable();

            mockChallengeQuery.Setup(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            mockChallengeQuery.SetupGet(challengeQuery => challengeQuery.Mapper).Returns(TestMapper).Verifiable();

            var controller = new ChallengesController(mockChallengeQuery.Object, mockChallengeService.Object);

            // Act
            var result = await controller.PostAsync(
                new CreateChallengeRequest(
                    new Guid(),
                    10,
                    5000,
                    "Token"));

            // Assert
            result.Should().BeOfType<OkObjectResult>();

            mockChallengeService.Verify(
                challengeService => challengeService.CreateChallengeAsync(
                    It.IsAny<ChallengeId>(),
                    It.IsAny<PayoutEntries>(),
                    It.IsAny<EntryFee>(),
                    It.IsAny<CancellationToken>()),
                Times.Once);

            mockChallengeQuery.Verify(challengeQuery => challengeQuery.FindChallengeAsync(It.IsAny<ChallengeId>()), Times.Once);

            mockChallengeQuery.VerifyGet(challengeQuery => challengeQuery.Mapper, Times.Once);
        }
    }
}
