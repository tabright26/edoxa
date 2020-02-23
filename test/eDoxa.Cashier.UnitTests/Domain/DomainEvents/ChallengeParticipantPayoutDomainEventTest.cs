// Filename: ChallengeParticipantPayoutDomainEventTest.cs
// Date Created: 2020-02-17
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.DomainEvents;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.DomainEvents
{
    public sealed class ChallengeParticipantPayoutDomainEventTest : UnitTest
    {
        public ChallengeParticipantPayoutDomainEventTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void ChallengeParticipantPayoutDomainEvent_WithMoney_ShouldBeValid()
        {
            // Arrange
            var userId = new UserId();
            var currency = new Money(50);
            var domainEvent = new ChallengeParticipantPayoutDomainEvent(userId, currency);

            // Assert
            domainEvent.UserId.Should().Be(userId);
            domainEvent.Currency.Should().Be(currency);
        }

        [Fact]
        public void ChallengeParticipantPayoutDomainEvent_WithToken_ShouldBeValid()
        {
            // Arrange
            var userId = new UserId();
            var currency = new Token(20);
            var domainEvent = new ChallengeParticipantPayoutDomainEvent(userId, currency);

            // Assert
            domainEvent.UserId.Should().Be(userId);
            domainEvent.Currency.Should().Be(currency);
        }
    }
}
