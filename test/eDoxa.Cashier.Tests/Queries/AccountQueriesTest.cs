// Filename: AccountQueriesTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Testing.MSTest;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.Queries
{
    [TestClass]
    public sealed class AccountQueriesTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<BalanceQuery>.For(
                    typeof(IAccountRepository),
                    typeof(IHttpContextAccessor),
                    typeof(IMapper)
                )
                .WithName("BalanceQuery")
                .Assert();
        }
    }
}
