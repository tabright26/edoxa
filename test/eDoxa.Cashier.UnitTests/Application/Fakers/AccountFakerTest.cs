// Filename: AccountFakerTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Fakers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Fakers
{
    [TestClass]
    public sealed class AccountFakerTest
    {
        [TestMethod]
        public void FakeNewAccount_ShouldNotThrow()
        {
            // Arrange
            var accountFaker = new AccountFaker();

            // Act
            var account = accountFaker.Generate();

            // Assert
            account.Should().NotBeNull();
        }

        [TestMethod]
        public void FakeAdminAccount_ShouldNotThrow()
        {
            // Arrange
            var accountFaker = new AccountFaker();

            // Act
            var account = accountFaker.Generate(AccountFaker.AdminAccount);

            // Assert
            account.Should().NotBeNull();
        }
    }
}
