// Filename: MoneyAccountServiceTest.cs
// Date Created: 2019-05-29
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

namespace eDoxa.Cashier.Tests.Services
{
    [TestClass]
    public sealed class AccountServiceTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<AccountService>.For(typeof(IAccountRepository), typeof(IStripeService)).WithName("AccountService").Assert();
        }
    }
}
