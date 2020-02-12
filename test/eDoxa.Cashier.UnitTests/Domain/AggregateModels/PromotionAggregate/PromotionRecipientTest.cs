// Filename: PromotionRecipientTest.cs
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
    public sealed class PromotionRecipientTest : UnitTest
    {
        public PromotionRecipientTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        private static User GenerateUser()
        {
            return new User(new UserId());
        }

        [Fact]
        public void ToString_WithPromotionRecipient_ShouldBeUserId()
        {
            var user = GenerateUser();

            var promotionRecipient = new PromotionRecipient(user, new DateTimeProvider(DateTime.UtcNow));

            promotionRecipient.ToString().Should().Be(user.Id);
        }
    }
}
