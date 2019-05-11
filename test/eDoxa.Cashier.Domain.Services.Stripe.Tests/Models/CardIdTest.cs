// Filename: CardIdTest.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Domain.Services.Stripe.Tests.Models
{
    [TestClass]
    public sealed class CardIdTest
    {
        [TestClass]
        public sealed class BankAccountIdTest
        {
            [TestMethod]
            public void Constructor_Tests()
            {
                ConstructorTests<CardId>.For(typeof(string))
                    .WithName("CardId")
                    .Assert();
            }
        }
    }
}