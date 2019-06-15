// Filename: AccountQueriesTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Api.Application.Queries;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Queries
{
    [TestClass]
    public sealed class AccountQueriesTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<BalanceQuery>.ForParameters(typeof(IAccountRepository), typeof(IHttpContextAccessor), typeof(IMapper)).WithClassName("BalanceQuery").Assert();
        }
    }
}
