// Filename: ChallengeServiceTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Application.Services;
using eDoxa.Cashier.Api.Application.Strategies;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
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
        public ChallengeServiceTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public async Task CreateChallengeAsync_ShouldBeOfTypeValidationResult()
        {
            // Arrange
            TestMock.ChallengePayoutFactory.Setup(payout => payout.CreateInstance()).Returns(new DefaultChallengePayoutStrategy()).Verifiable();

            TestMock.ChallengeRepository.Setup(repository => repository.Create(It.IsAny<Challenge>())).Verifiable();

            TestMock.ChallengeRepository.Setup(repository => repository.CommitAsync(true, It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask)
                .Verifiable();

            var service = new ChallengeService(TestMock.ChallengePayoutFactory.Object, TestMock.ChallengeRepository.Object);

            var bucket = new ChallengePayoutBucket(ChallengePayoutBucketPrize.Consolation, ChallengePayoutBucketSize.Individual);

            var buckets = new ChallengePayoutBuckets(
                new List<ChallengePayoutBucket>
                {
                    bucket
                });

            var payoutEntries = new ChallengePayoutEntries(buckets);

            // Act
            var result = await service.CreateChallengeAsync(new ChallengeId(), payoutEntries, new EntryFee(5000, CurrencyType.Token));

            // Assert
            result.Should().BeOfType<DomainValidationResult<IChallenge>>();

            TestMock.ChallengePayoutFactory.Verify(payout => payout.CreateInstance(), Times.Once);

            TestMock.ChallengeRepository.Verify(repository => repository.Create(It.IsAny<Challenge>()), Times.Once);

            TestMock.ChallengeRepository.Verify(repository => repository.CommitAsync(true, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task CreateChallengeAsync_ShouldBeOfTypeValidationResultWithErrors()
        {
            // Arrange
            TestMock.ChallengePayoutStrategy.Setup(payout => payout.GetChallengePayout(It.IsAny<ChallengePayoutEntries>(), It.IsAny<EntryFee>())).Verifiable();

            TestMock.ChallengePayoutFactory.Setup(payout => payout.CreateInstance()).Returns(TestMock.ChallengePayoutStrategy.Object).Verifiable();

            var service = new ChallengeService(TestMock.ChallengePayoutFactory.Object, TestMock.ChallengeRepository.Object);

            var bucket = new ChallengePayoutBucket(ChallengePayoutBucketPrize.Consolation, ChallengePayoutBucketSize.Individual);

            var buckets = new ChallengePayoutBuckets(
                new List<ChallengePayoutBucket>
                {
                    bucket
                });

            var payoutEntries = new ChallengePayoutEntries(buckets);

            // Act
            var result = await service.CreateChallengeAsync(new ChallengeId(), payoutEntries, new EntryFee(5000, CurrencyType.Token));

            // Assert
            result.Should().BeOfType<DomainValidationResult<IChallenge>>();

            result.Errors.Should().NotBeEmpty();

            TestMock.ChallengePayoutStrategy.Verify(payout => payout.GetChallengePayout(It.IsAny<ChallengePayoutEntries>(), It.IsAny<EntryFee>()), Times.Once);

            TestMock.ChallengePayoutFactory.Verify(payout => payout.CreateInstance(), Times.Once);
        }

        [Fact]
        public async Task FindChallengeAsync_ShouldBeOfTypeChallenge()
        {
            // Arrange
            var challenge = TestData.FakerFactory.CreateChallengeFaker(1000).FakeChallenge();

            TestMock.ChallengeRepository.Setup(repository => repository.FindChallengeOrNullAsync(It.IsAny<ChallengeId>())).ReturnsAsync(challenge).Verifiable();

            var service = new ChallengeService(TestMock.ChallengePayoutFactory.Object, TestMock.ChallengeRepository.Object);

            // Act
            var result = await service.FindChallengeOrNullAsync(new ChallengeId());

            // Assert
            result.Should().BeOfType<Challenge>();

            TestMock.ChallengeRepository.Verify(repository => repository.FindChallengeOrNullAsync(It.IsAny<ChallengeId>()), Times.Once);
        }
    }
}
