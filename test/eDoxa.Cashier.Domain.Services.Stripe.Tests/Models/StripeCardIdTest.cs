// Filename: StripeCardIdTest.cs
// Date Created: 2019-05-13
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
    public sealed class StripeCardIdTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<StripeCardId>.For(typeof(string))
                .WithName("StripeCardId")
                .Assert();
        }
    }
}