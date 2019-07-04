﻿// Filename: AccountFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Api.Application.Fakers;
using eDoxa.Seedwork.Common.Extensions;

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
            var action = new Action(
                () =>
                {
                    var account = accountFaker.Generate();

                    Console.WriteLine(account.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeAdminAccount_ShouldNotThrow()
        {
            // Arrange
            var accountFaker = new AccountFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var account = accountFaker.FakeAdminAccount();

                    Console.WriteLine(account.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }
    }
}
