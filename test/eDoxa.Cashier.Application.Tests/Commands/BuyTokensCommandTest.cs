// Filename: BuyTokensCommandTest.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.Commands
{
    [TestClass]
    public sealed class BuyTokensCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<BuyTokensCommand>.For(typeof(TokenBundleType))
                .WithName("BuyTokensCommand")
                .Assert();
        }
    }
}