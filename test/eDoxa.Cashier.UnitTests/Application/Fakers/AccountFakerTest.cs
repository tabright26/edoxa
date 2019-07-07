// Filename: AccountFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api.Application.Fakers;

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
