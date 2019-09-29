// Filename: AccountFakerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;
using eDoxa.Cashier.UnitTests.TestHelpers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Fakers
{
    public sealed class AccountFakerTest : UnitTestClass
    {
        public AccountFakerTest(TestDataFixture testData) : base(testData)
        {
        }

        [Fact]
        public void FakeAccount_FakeNewAccount_ShouldNotBeNull()
        {
            // Arrange
            var accountFaker = TestData.FakerFactory.CreateAccountFaker(null);

            // Act
            var account = accountFaker.FakeAccount();

            // Assert
            account.Should().NotBeNull();
        }

        [Fact]
        public void Generate_FakeAdminAccount_ShouldNotBeNull()
        {
            // Arrange
            var accountFaker = TestData.FakerFactory.CreateAccountFaker(null);

            // Act
            var account = accountFaker.FakeAccount(AccountFaker.AdminAccount);

            // Assert
            account.Should().NotBeNull();
        }
    }
}
