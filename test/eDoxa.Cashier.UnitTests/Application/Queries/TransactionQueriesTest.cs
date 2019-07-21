// Filename: TransactionQueriesTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Cashier.Api.Infrastructure.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.AspNetCore.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Queries
{
    [TestClass]
    public sealed class TransactionQueriesTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<TransactionQuery>.ForParameters(typeof(CashierDbContext), typeof(IHttpContextAccessor), typeof(IMapper))
                .WithClassName("TransactionQuery")
                .Assert();
        }
    }
}
