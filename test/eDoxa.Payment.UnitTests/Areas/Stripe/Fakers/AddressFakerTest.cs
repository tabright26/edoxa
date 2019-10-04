// Filename: AddressFakerTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.Areas.Stripe.Fakers;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Payment.UnitTests.Areas.Stripe.Fakers
{
    // TODO: These tests must be recast to be more explicit about the tested behavior. (Check if Bogus has a special librairy to test fakers.)
    public sealed class AddressFakerTest : UnitTest
    {
        public AddressFakerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void FakeAddress_ShouldNotBeNull()
        {
            // Arrange
            var addressFaker = new AddressFaker();

            // Act
            var address = addressFaker.FakeAddress();

            // Assert
            address.Should().NotBeNull();
        }
    }
}
