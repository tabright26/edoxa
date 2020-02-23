// Filename: PromotionRedeemedDomainEventTest.cs
// Date Created: 2020-02-17
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.Domain.DomainEvents;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.DomainEvents
{
    public sealed class PromotionRedeemedDomainEventTest : UnitTest
    {
        public PromotionRedeemedDomainEventTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void ChallengeClosedDomainEvent_WithMoney_ShouldBeValid()
        {
            // Arrange
            var userId = new UserId();
            var promotionId = new PromotionId();
            var currencyType = CurrencyType.Money;
            const decimal amount = 50.0m;

            var domainEvent = new PromotionRedeemedDomainEvent(
                userId,
                promotionId,
                currencyType,
                amount);

            // Assert
            domainEvent.UserId.Should().Be(userId);
            domainEvent.PromotionId.Should().Be(promotionId);
            domainEvent.CurrencyType.Should().Be(currencyType);
            domainEvent.Amount.Should().Be(amount);
        }

        [Fact]
        public void ChallengeClosedDomainEvent_WithToken_ShouldBeValid()
        {
            // Arrange
            var userId = new UserId();
            var promotionId = new PromotionId();
            var currencyType = CurrencyType.Token;
            const decimal amount = 20.0m;

            var domainEvent = new PromotionRedeemedDomainEvent(
                userId,
                promotionId,
                currencyType,
                amount);

            // Assert
            domainEvent.UserId.Should().Be(userId);
            domainEvent.PromotionId.Should().Be(promotionId);
            domainEvent.CurrencyType.Should().Be(currencyType);
            domainEvent.Amount.Should().Be(amount);
        }
    }
}
