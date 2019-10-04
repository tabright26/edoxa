// Filename: CustomerFakerTest.cs
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
    public sealed class CustomerFakerTest : UnitTest
    {
        public CustomerFakerTest(TestMapperFixture testMapper) : base(testMapper)
        {
        }

        [Fact]
        public void FakeCustomer_ShouldNotBeNull()
        {
            // Arrange
            var customerFaker = new CustomerFaker();

            // Act
            var customer = customerFaker.FakeCustomer();

            // Assert
            customer.Should().NotBeNull();
        }
    }
}
