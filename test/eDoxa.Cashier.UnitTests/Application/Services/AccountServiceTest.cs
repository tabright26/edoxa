// Filename: AccountServiceTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Application.Services;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.ServiceBus;
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
            TestConstructor<AccountService>.ForParameters(typeof(IAccountRepository), typeof(IIntegrationEventPublisher))
                .WithClassName("AccountService")
                .Assert();
        }
    }
}
