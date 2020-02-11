// Filename: PromotionTest.cs
// Date Created: 2020-02-04
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

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
            _validPromotionEndDate = new DateTimeProvider(DateTime.UtcNow.AddDays(30));
            _invalidPromotionEndDate = new DateTimeProvider(DateTime.UtcNow);
            _promotionTime = TimeSpan.FromDays(30);
        }

        private const string TestCode = "TestCode";

        private readonly DateTimeProvider _validPromotionEndDate;
        private readonly DateTimeProvider _invalidPromotionEndDate;
        private readonly TimeSpan _promotionTime;

        private static User GenerateUser()
        {
            return new User(new UserId());
        }

        [Fact]

        public void Cancel_WhenPromotionIsActive_ShouldCancel()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _invalidPromotionEndDate
                );

            var cancelTime = new DateTimeProvider(DateTime.UtcNow);

            promotion.Cancel(cancelTime);

            promotion.CanceledAt.Should().Be(cancelTime.DateTime);
        }

        [Fact]
        public void Cancel_WhenPromotionIsExpired_ShouldThrowInvalidOperationException()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _invalidPromotionEndDate);

            var cancelTime = new DateTimeProvider(DateTime.UtcNow);

            var action = new Action(() => promotion.Cancel(cancelTime));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void IsActive_WhenPromotionIsActive_ShouldBeTrue()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _validPromotionEndDate);

            promotion.IsActive().Should().BeTrue();
        }

        [Fact]
        public void IsActive_WhenPromotionIsExpired_ShouldBeFalse()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _invalidPromotionEndDate);

            promotion.IsActive().Should().BeFalse();
        }

        [Fact]
        public void IsCanceled_WhenPromotionIsActive_ShouldBeTrue()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _validPromotionEndDate);

            promotion.Cancel(new DateTimeProvider(DateTime.UtcNow));
            promotion.IsCanceled().Should().BeTrue();
        }

        [Fact]
        public void IsCanceled_WhenPromotionIsNew_ShouldBeFalse()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _validPromotionEndDate);

            promotion.IsCanceled().Should().BeFalse();
        }

        [Fact]
        public void IsExpired_WhenPromotionIsExpired_ShouldBeTrue()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _invalidPromotionEndDate);

            promotion.IsExpired().Should().BeTrue();
        }

        [Fact]
        public void IsExpired_WhenPromotionIsNew_ShouldBeFalse()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _validPromotionEndDate);

            promotion.IsExpired().Should().BeFalse();
        }

        [Fact]
        public void IsRedeemBy_WhenPromotionIsNew_ShouldBeFalse()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _validPromotionEndDate);

            var promotionRecipient = new PromotionRecipient(GenerateUser(), new DateTimeProvider(DateTime.UtcNow));
            promotion.IsRedeemBy(promotionRecipient).Should().BeFalse();
        }

        [Fact]
        public void IsRedeemBy_WhenAlreadyRedeemed_ShouldBeTrue()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _validPromotionEndDate);

            var promotionRecipient = new PromotionRecipient(GenerateUser(), new DateTimeProvider(DateTime.UtcNow));

            promotion.Redeem(promotionRecipient);

            promotion.IsRedeemBy(promotionRecipient).Should().BeTrue();
        }

        [Fact]
        public void Redeem_WhenPromotionIsExpired_ShouldThrowInvalidOperationException()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _invalidPromotionEndDate);

            var promotionRecipient = new PromotionRecipient(GenerateUser(), new DateTimeProvider(DateTime.UtcNow));

            var action = new Action(() => promotion.Redeem(promotionRecipient));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Redeem_WhenPromotionIsActive_ShouldHaveCountOfOne()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                _promotionTime,
                _validPromotionEndDate);

            var promotionRecipient = new PromotionRecipient(GenerateUser(), new DateTimeProvider(DateTime.UtcNow));

            promotion.Redeem(promotionRecipient);

            promotion.Recipients.Should().HaveCount(1);
        }
    }
}
