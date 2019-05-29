// Filename: MoneyAccountServiceTest.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Services;
using eDoxa.Cashier.Services.Stripe.Abstractions;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests
{
    [TestClass]
    public sealed class MoneyAccountServiceTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<MoneyAccountService>.For(typeof(IMoneyAccountRepository), typeof(IStripeService)).WithName("MoneyAccountService").Assert();
        }
    }
}
