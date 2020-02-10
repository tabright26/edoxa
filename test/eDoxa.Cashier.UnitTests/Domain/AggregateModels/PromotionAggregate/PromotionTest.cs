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
        }

        private const string TestCode = "TestCode";

        private static User GenerateUser()
        {
            return new User(new UserId());
        }

        [Fact]

        // Francis: Comment je devrais nommer cette methode la ???
        public void Cancel_IfCanCancel_ShouldCancel()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(30),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            var cancelTime = new DateTimeProvider(DateTime.Now);

            promotion.Cancel(cancelTime);

            promotion.CanceledAt.Should().Be(cancelTime.DateTime);
        }

        [Fact]
        public void Cancel_IfCantCancel_ShouldThrowInvalidOperationException()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(0),
                new DateTimeProvider(DateTime.Now));

            var cancelTime = new DateTimeProvider(DateTime.Now);

            var action = new Action(() => promotion.Cancel(cancelTime));
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void IsActive_WhenActive_ShouldBeTrue()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(30),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            promotion.IsActive().Should().BeTrue();
        }

        [Fact]
        public void IsActive_WhenNotActive_ShouldBeFalse()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(5),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            promotion.IsActive().Should().BeFalse();
        }

        [Fact]
        public void IsCanceled_WhenCancelled_ShouldBeTrue()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(30),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            promotion.Cancel(new DateTimeProvider(DateTime.Now));
            promotion.IsCanceled().Should().BeTrue();
        }

        [Fact]
        public void IsCanceled_WhenNotCancelled_ShouldBeFalse()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(30),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            promotion.IsCanceled().Should().BeFalse();
        }

        [Fact]
        public void IsExpired_WhenExpired_ShouldBeTrue()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(2),
                new DateTimeProvider(DateTime.Now));

            promotion.IsExpired().Should().BeTrue();
        }

        [Fact]
        public void IsExpired_WhenNotExpired_ShouldBeFalse()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(30),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            promotion.IsExpired().Should().BeFalse();
        }

        [Fact]
        public void IsRedeemBy_WhenEmpty_ShouldBeFalse()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(30),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            var promotionRecipient = new PromotionRecipient(GenerateUser(), new DateTimeProvider(DateTime.Now));
            promotion.IsRedeemBy(promotionRecipient).Should().BeFalse();
        }

        [Fact]
        public void IsRedeemBy_WhenRedeemed_ShouldBeTrue()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(30),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            var promotionRecipient = new PromotionRecipient(GenerateUser(), new DateTimeProvider(DateTime.Now));

            promotion.Redeem(promotionRecipient);

            promotion.IsRedeemBy(promotionRecipient).Should().BeTrue();
        }

        [Fact]
        public void Redeem_WithInvalidPromotion_ShouldThrowInvalidOperationException()
        {
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(0),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            var promotionRecipient = new PromotionRecipient(GenerateUser(), new DateTimeProvider(DateTime.Now));

            var action = new Action(() => promotion.Redeem(promotionRecipient));

            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Redeem_WithValidPromotion_ShouldHaveCountOfOne()
        {
            // Francis: Pourquoi est-ce que il y a une duration et une date d<expiration, se serait pas mieux une date de début et une duration ????
            //Aussi, UtcDateTimeProvider est briser, il est toujours une journée dans le futur.
            var promotion = new Promotion(
                TestCode,
                new Money(50),
                TimeSpan.FromDays(30),
                new DateTimeProvider(DateTime.Now.AddDays(30)));

            var promotionRecipient = new PromotionRecipient(GenerateUser(), new DateTimeProvider(DateTime.Now));

            promotion.Redeem(promotionRecipient);

            promotion.Recipients.Should().HaveCount(1);
        }
    }
}
