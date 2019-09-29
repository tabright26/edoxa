// Filename: AccountFakerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.Providers.Stripe.Fakers;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Payment.UnitTests.Providers.Stripe.Fakers
{
    // TODO: These tests must be recast to be more explicit about the tested behavior. (Check if Bogus has a special librairy to test fakers.)
    public sealed class AccountFakerTest : UnitTest
    {
        public AccountFakerTest(TestDataFixture testData) : base(testData)
        {
        }

        [Fact]
        public void FakeAccount_ShouldNotBeNull()
        {
            // Arrange
            var accountFaker = new AccountFaker();

            // Act
            var account = accountFaker.FakeAccount();

            // Assert 
            account.Should().NotBeNull();
        }
    }
}
