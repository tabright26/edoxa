// Filename: AccountRepositoryTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Repositories;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Repositories
{
    [TestClass]
    public sealed class AccountRepositoryTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<AccountRepository>.ForParameters(typeof(CashierDbContext), typeof(IMapper)).WithClassName("AccountRepository").Assert();
        }
    }
}
