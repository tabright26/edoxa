// Filename: MoneyAccountRepositoryTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Testing.MSTest.Constructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.Infrastructure.Repositories
{
    [TestClass]
    public sealed class AccountRepositoryTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<AccountRepository>.For(typeof(CashierDbContext)).WithName("AccountRepository").Assert();
        }
    }
}
