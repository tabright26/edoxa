﻿// Filename: StripeConnectAccountIdTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Testing.TestConstructor;
using eDoxa.Stripe.Exceptions;
using eDoxa.Stripe.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Stripe.UnitTests.Models
{
    [TestClass]
    public sealed class StripeConnectAccountIdTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "Expected Stripe Connect AccountId is invalid.";

            TestConstructor<StripeConnectAccountId>.ForParameters(typeof(string))
                .WithClassName("StripeConnectAccountId")
                .Failure(new object[] {null}, typeof(StripeIdException), message)
                .Failure(new object[] {"  "}, typeof(StripeIdException), message)
                .Failure(new object[] {"acct_23Eri2_ee23"}, typeof(StripeIdException), message)
                .Failure(new object[] {"acct23Eri2ee23"}, typeof(StripeIdException), message)
                .Failure(new object[] {"23Eri2_ee23"}, typeof(StripeIdException), message)
                .Failure(new object[] {"test_23Eri2ee23"}, typeof(StripeIdException), message)
                .Failure(new object[] {"acct_we23we$"}, typeof(StripeIdException), message)
                .Failure(new object[] {"acct_@$Eri2ee23"}, typeof(StripeIdException), message)
                .Failure(new object[] {"acct_trEr%2ee23"}, typeof(StripeIdException), message)
                .Success(new object[] {"acct_23Eri2ee23"}, message)
                .Success(new object[] {"acct_er34ri2ee23"}, message)
                .Assert();
        }
    }
}
