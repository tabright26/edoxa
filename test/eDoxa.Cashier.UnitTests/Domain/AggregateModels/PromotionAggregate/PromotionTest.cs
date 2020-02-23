// Filename: PromotionTest.cs
// Date Created: 2020-01-28
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.PromotionAggregate;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.AggregateModels.PromotionAggregate
{
    public sealed class PromotionTest : UnitTest
    {
        public PromotionTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }

        private Promotion GenerateValidPromotion()
        {
            return new Promotion("TestPromoCode", new Token(100000), TimeSpan.FromDays(30), new DateTimeProvider(DateTime.UtcNow.AddDays(30)));
        }

        private Promotion GenerateExpiredPromotion()
        {
            return new Promotion("TestPromoCode", new Token(100000), TimeSpan.FromDays(30), new UtcNowDateTimeProvider());
        }

        private PromotionRecipient GeneratePromotionRecipient()
        {
            return new PromotionRecipient(new User(new UserId()), new UtcNowDateTimeProvider());
        }

        [Fact]
        public void Redeem_WhenValid_ShouldCreateDomainEvent()
        {
            // Arrange
            var recipient = this.GeneratePromotionRecipient();
            var promotion = this.GenerateValidPromotion();

            // Act
            promotion.Redeem(recipient);

            // Assert
            promotion.Recipients.Count.Should().Be(1);
            promotion.DomainEvents.Count.Should().Be(1);
            promotion.IsRedeemBy(recipient).Should().BeTrue();
            var result = promotion.Recipients.First();
            result.ToString().Should().Be(recipient.User.Id);
        }

        [Fact]
        public void Redeem_WhenExpired_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var recipient = this.GeneratePromotionRecipient();
            var promotion = this.GenerateExpiredPromotion();

            // Act Assert
            var action = new Action(() => promotion.Redeem(recipient));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Cancel_WhenValid_ShouldCreateDomainEvent()
        {
            // Arrange
            var promotion = this.GenerateValidPromotion();
            var cancelTime = new UtcNowDateTimeProvider();

            // Act
            promotion.Cancel(cancelTime);

            // Assert
            promotion.CanceledAt.Should().Be(cancelTime.DateTime);
        }

        [Fact]
        public void Cancel_WhenExpired_ShouldThrowInvalidOperationException()
        {
            // Arrange
            var promotion = this.GenerateExpiredPromotion();

            // Act Assert
            var action = new Action(() => promotion.Cancel(new UtcNowDateTimeProvider()));
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
