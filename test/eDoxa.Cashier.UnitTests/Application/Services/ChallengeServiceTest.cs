// Filename: ChallengeServiceTest.cs
// Date Created: 2019-11-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Services;
using eDoxa.Cashier.Api.Application.Strategies;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.Factories;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Domain.Strategies;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Application.Services
{
    public sealed class ChallengeServiceTest : UnitTest
    {
        public ChallengeServiceTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task CreateChallengeAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            var mockChallengePayoutFactory = new Mock<IChallengePayoutFactory>();
            var mockChallengeRepository = new Mock<IChallengeRepository>();

            mockChallengePayoutFactory.Setup(payout => payout.CreateInstance()).Returns(new ChallengePayoutStrategy()).Verifiable();

            mockChallengeRepository.Setup(repository => repository.Create(It.IsAny<Challenge>())).Verifiable();

            mockChallengeRepository.Setup(repository => repository.CommitAsync(It.IsAny<CancellationToken>())).Returns(Task.CompletedTask).Verifiable();

            var service = new ChallengeService(mockChallengePayoutFactory.Object, mockChallengeRepository.Object);

            var bucket = new Bucket(Prize.None, BucketSize.Individual);

            var buckets = new Buckets(
                new List<Bucket>
                {
                    bucket
                });

            var payoutEntries = new PayoutEntries(buckets);

            // Act
            var result = await service.CreateChallengeAsync(new ChallengeId(), payoutEntries, new EntryFee(5000, Currency.Token));

            // Assert
            result.Should().BeOfType<DomainValidationResult>();

            mockChallengePayoutFactory.Verify(payout => payout.CreateInstance(), Times.Once);

            mockChallengeRepository.Verify(repository => repository.Create(It.IsAny<Challenge>()), Times.Once);

            mockChallengeRepository.Verify(repository => repository.CommitAsync(It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateChallengeAsync_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            var mockChallengePayoutFactory = new Mock<IChallengePayoutFactory>();
            var mockChallengeRepository = new Mock<IChallengeRepository>();

            var mockChallengePayoutStrategy = new Mock<IChallengePayoutStrategy>();

            mockChallengePayoutStrategy.Setup(payout => payout.GetPayout(It.IsAny<PayoutEntries>(), It.IsAny<EntryFee>())).Verifiable();

            mockChallengePayoutFactory.Setup(payout => payout.CreateInstance()).Returns(mockChallengePayoutStrategy.Object).Verifiable();

            var service = new ChallengeService(mockChallengePayoutFactory.Object, mockChallengeRepository.Object);

            var bucket = new Bucket(Prize.None, BucketSize.Individual);

            var buckets = new Buckets(
                new List<Bucket>
                {
                    bucket
                });

            var payoutEntries = new PayoutEntries(buckets);

            // Act
            var result = await service.CreateChallengeAsync(new ChallengeId(), payoutEntries, new EntryFee(5000, Currency.Token));

            // Assert
            result.Should().BeOfType<DomainValidationResult>();
            result.Errors.Should().NotBeEmpty();

            mockChallengePayoutStrategy.Verify(payout => payout.GetPayout(It.IsAny<PayoutEntries>(), It.IsAny<EntryFee>()), Times.Once);
            mockChallengePayoutFactory.Verify(payout => payout.CreateInstance(), Times.Once);
        }

        [Fact]
        public async Task FindChallengeAsync_ShouldBeOfTypeChallenge()
        {
            // Arrange
            var challenge = TestData.FakerFactory.CreateChallengeFaker(1000).FakeChallenge();

            var mockChallengePayoutFactory = new Mock<IChallengePayoutFactory>();

            var mockChallengeRepository = new Mock<IChallengeRepository>();

            mockChallengeRepository.Setup(repository => repository.FindChallengeOrNullAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            var service = new ChallengeService(mockChallengePayoutFactory.Object, mockChallengeRepository.Object);

            // Act
            var result = await service.FindChallengeOrNullAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<Challenge>();
            mockChallengeRepository.Verify(repository => repository.FindChallengeOrNullAsync(It.IsAny<ChallengeId>()), Times.Once);
        }
    }
}
