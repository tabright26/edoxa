// Filename: AccountServiceTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright � 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api.Application.Services;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Services
{
    [TestClass]
    public sealed class AccountServiceTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<AccountService>.ForParameters(typeof(IAccountRepository),typeof(IIntegrationEventService)).WithClassName("AccountService").Assert();
        }
    }
}