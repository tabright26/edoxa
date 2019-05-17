// Filename: TransactionQueriesTest.cs
// Date Created: 2019-05-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Cashier.Application.Abstractions;
using eDoxa.Cashier.Application.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.Queries
{
    [TestClass]
    public sealed class TransactionQueriesTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<TransactionQueries>.For(typeof(CashierDbContext), typeof(ICashierSecurity), typeof(IMapper))
                .WithName("TransactionQueries")
                .Assert();
        }
    }
}