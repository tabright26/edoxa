// Filename: PromotionControllerTest.cs
// Date Created: 2020-02-04
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Controllers;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Cashier.Enums;
using eDoxa.Grpc.Protos.Cashier.Requests;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.TestHelper.Mocks;

using FluentAssertions;

using Google.Protobuf.WellKnownTypes;

using Microsoft.AspNetCore.Mvc;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Controllers
{
    public sealed class PromotionControllerTest : UnitTest
    {
        public PromotionControllerTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        private const string TestCode = "TestCode";

        private static Promotion GeneratePromotion()
        {
            return new Promotion(
                "code1",
                new Money(50),
                TimeSpan.FromDays(1),
                new UtcNowDateTimeProvider());
        }

        private static IReadOnlyCollection<Promotion> GeneratePromotions()
        {
            return new List<Promotion>
            {
                new Promotion(
                    "code1",
                    new Money(50),
                    TimeSpan.FromDays(1),
                    new UtcNowDateTimeProvider()),
                new Promotion(
                    "code2",
                    new Token(20),
                    TimeSpan.FromDays(2),
                    new UtcNowDateTimeProvider()),
                new Promotion(
                    "code3",
                    new Money(20),
                    TimeSpan.FromDays(3),
                    new UtcNowDateTimeProvider())
            }.ToImmutableList();
        }

        [Fact]
        public async Task CreatePromotionAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.CreatePromotionAsync(
                        It.IsAny<string>(),
                        It.IsAny<Currency>(),
                        It.IsAny<TimeSpan>(),
                        It.IsAny<DateTime>()))
                .ReturnsAsync(DomainValidationResult<Promotion>.Failure("error message"))
                .Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper);

            var request = new CreatePromotionRequest
            {
                Currency = new CurrencyDto
                {
                    Amount = 50,
                    Type = EnumCurrencyType.Money
                },
                Duration = new Duration(),
                ExpiredAt = DateTime.UtcNow.ToTimestamp(),
                PromotionalCode = "code1"
            };

            // Act
            var result = await controller.CreatePromotionAsync(request);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.PromotionService.Verify(
                promotionService => promotionService.CreatePromotionAsync(
                    It.IsAny<string>(),
                    It.IsAny<Currency>(),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<DateTime>()),
                Times.Once);
        }

        [Fact]
        public async Task CreatePromotionAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.CreatePromotionAsync(
                        It.IsAny<string>(),
                        It.IsAny<Currency>(),
                        It.IsAny<TimeSpan>(),
                        It.IsAny<DateTime>()))
                .ReturnsAsync(DomainValidationResult<Promotion>.Succeeded(GeneratePromotion()))
                .Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper);

            var request = new CreatePromotionRequest
            {
                Currency = new CurrencyDto
                {
                    Amount = 50,
                    Type = EnumCurrencyType.Money
                },
                Duration = new Duration(),
                ExpiredAt = DateTime.UtcNow.ToTimestamp(),
                PromotionalCode = TestCode
            };

            // Act
            var result = await controller.CreatePromotionAsync(request);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<PromotionDto>(GeneratePromotion()));

            TestMock.PromotionService.Verify(
                promotionService => promotionService.CreatePromotionAsync(
                    It.IsAny<string>(),
                    It.IsAny<Currency>(),
                    It.IsAny<TimeSpan>(),
                    It.IsAny<DateTime>()),
                Times.Once);
        }

        [Fact]
        public async Task FetchPromotionsAsync_ShouldBeNoContentResult()
        {
            // Arrange
            TestMock.PromotionService.Setup(promotionService => promotionService.FetchPromotionsAsync()).ReturnsAsync(new List<Promotion>()).Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper);

            // Act
            var result = await controller.FetchPromotionsAsync();

            // Assert
            result.Should().BeOfType<NoContentResult>();
            TestMock.PromotionService.Verify(promotionService => promotionService.FetchPromotionsAsync(), Times.Once);
        }

        [Fact]
        public async Task FetchPromotionsAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            TestMock.PromotionService.Setup(promotionService => promotionService.FetchPromotionsAsync()).ReturnsAsync(GeneratePromotions()).Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper);

            // Act
            var result = await controller.FetchPromotionsAsync();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<PromotionDto[]>(GeneratePromotions()));
            TestMock.PromotionService.Verify(promotionService => promotionService.FetchPromotionsAsync(), Times.Once);
        }

        [Fact]
        public async Task RedeemPromotionAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var promotion = GeneratePromotion();

            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.FindPromotionOrNullAsync(
                        It.IsAny<string>()))
                .ReturnsAsync(promotion)
                .Verifiable();

            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.RedeemPromotionAsync(
                        It.IsAny<Promotion>(), It.IsAny<UserId>(), It.IsAny<IDateTimeProvider>()))
                .ReturnsAsync(DomainValidationResult<Promotion>.Succeeded(promotion))
                .Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await controller.RedeemPromotionAsync(TestCode);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<PromotionDto>(promotion));

            TestMock.PromotionService.Verify(
                promotionService => promotionService.FindPromotionOrNullAsync(
                    It.IsAny<string>()),
                Times.Once);

            TestMock.PromotionService.Verify(
                promotionService => promotionService.RedeemPromotionAsync(
                    It.IsAny<Promotion>(), It.IsAny<UserId>(), It.IsAny<IDateTimeProvider>()),
                Times.Once);
        }

        [Fact]
        public async Task RedeemPromotionAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.FindPromotionOrNullAsync(
                        It.IsAny<string>()))
                .Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await controller.RedeemPromotionAsync(TestCode);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            TestMock.PromotionService.Verify(
                promotionService => promotionService.FindPromotionOrNullAsync(
                    It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task RedeemPromotionAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var promotion = GeneratePromotion();

            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.FindPromotionOrNullAsync(
                        It.IsAny<string>()))
                .ReturnsAsync(promotion)
                .Verifiable();

            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.RedeemPromotionAsync(
                        It.IsAny<Promotion>(), It.IsAny<UserId>(), It.IsAny<IDateTimeProvider>()))
                .ReturnsAsync(DomainValidationResult<Promotion>.Failure("error message"))
                .Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await controller.RedeemPromotionAsync(TestCode);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.PromotionService.Verify(
                promotionService => promotionService.FindPromotionOrNullAsync(
                    It.IsAny<string>()),
                Times.Once);

            TestMock.PromotionService.Verify(
                promotionService => promotionService.RedeemPromotionAsync(
                    It.IsAny<Promotion>(), It.IsAny<UserId>(), It.IsAny<IDateTimeProvider>()),
                Times.Once);
        }

        [Fact]
        public async Task CancelPromotionAsync_ShouldBeOkObjectResult()
        {
            // Arrange
            var promotion = GeneratePromotion();

            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.FindPromotionOrNullAsync(
                        It.IsAny<string>()))
                .ReturnsAsync(promotion)
                .Verifiable();

            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.CancelPromotionAsync(
                        It.IsAny<Promotion>(), It.IsAny<IDateTimeProvider>()))
                .ReturnsAsync(DomainValidationResult<Promotion>.Succeeded(promotion))
                .Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await controller.CancelPromotionAsync(TestCode);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            result.As<OkObjectResult>().Value.Should().BeEquivalentTo(TestMapper.Map<PromotionDto>(promotion));

            TestMock.PromotionService.Verify(
                promotionService => promotionService.FindPromotionOrNullAsync(
                    It.IsAny<string>()),
                Times.Once);

            TestMock.PromotionService.Verify(
                promotionService => promotionService.CancelPromotionAsync(
                    It.IsAny<Promotion>(), It.IsAny<IDateTimeProvider>()),
                Times.Once);
        }

        [Fact]
        public async Task CancelPromotionAsync_ShouldBeNotFoundObjectResult()
        {
            // Arrange
            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.FindPromotionOrNullAsync(
                        It.IsAny<string>()))
                .Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await controller.CancelPromotionAsync(TestCode);

            // Assert
            result.Should().BeOfType<NotFoundObjectResult>();
            TestMock.PromotionService.Verify(
                promotionService => promotionService.FindPromotionOrNullAsync(
                    It.IsAny<string>()),
                Times.Once);
        }

        [Fact]
        public async Task CancelPromotionAsync_ShouldBeBadRequestObjectResult()
        {
            // Arrange
            var promotion = GeneratePromotion();

            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.FindPromotionOrNullAsync(
                        It.IsAny<string>()))
                .ReturnsAsync(promotion)
                .Verifiable();

            TestMock.PromotionService
                .Setup(
                    promotionService => promotionService.CancelPromotionAsync(
                        It.IsAny<Promotion>(), It.IsAny<IDateTimeProvider>()))
                .ReturnsAsync(DomainValidationResult<Promotion>.Failure("error message"))
                .Verifiable();

            var controller = new PromotionController(TestMock.PromotionService.Object, TestMapper)
            {
                ControllerContext =
                {
                    HttpContext = MockHttpContextAccessor.GetInstance()
                }
            };

            // Act
            var result = await controller.CancelPromotionAsync(TestCode);

            // Assert
            result.Should().BeOfType<BadRequestObjectResult>();

            TestMock.PromotionService.Verify(
                promotionService => promotionService.FindPromotionOrNullAsync(
                    It.IsAny<string>()),
                Times.Once);

            TestMock.PromotionService.Verify(
                promotionService => promotionService.CancelPromotionAsync(
                    It.IsAny<Promotion>(), It.IsAny<IDateTimeProvider>()),
                Times.Once);
        }
    }
}
