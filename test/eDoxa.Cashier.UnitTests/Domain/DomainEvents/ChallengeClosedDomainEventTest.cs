// Filename: ChallengeClosedDomainEventTest.cs
// Date Created: 2020-02-17
//
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.DomainEvents;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Domain.DomainEvents
{
    public sealed class ChallengeClosedDomainEventTest : UnitTest
    {
        public ChallengeClosedDomainEventTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void ChallengeClosedDomainEvent_ShouldBeValid()
        {
            // Arrange
            var challengeId = new ChallengeId();
            var payouts = new ChallengeParticipantPayouts();
            var domainEvent = new ChallengeClosedDomainEvent(challengeId, payouts);

            // Assert
            domainEvent.ChallengeId.Should().Be(challengeId);
            domainEvent.Payouts.Should().BeEquivalentTo(payouts);
        }
    }
}
