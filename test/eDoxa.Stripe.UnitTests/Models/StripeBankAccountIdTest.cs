// Filename: StripeBankAccountIdTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Testing.Constructor;
using eDoxa.Stripe.Exceptions;
using eDoxa.Stripe.Models;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Stripe.UnitTests.Models
{
    [TestClass]
    public sealed class StripeBankAccountIdTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "Expected Stripe BankAccountId is invalid.";

            ConstructorTests<StripeBankAccountId>.For(typeof(string))
                .WithName("StripeBankAccountId")
                .Fail(new object[] {null}, typeof(StripeIdException), message)
                .Fail(new object[] {"  "}, typeof(StripeIdException), message)
                .Fail(new object[] {"ba_23Eri2_ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"ba23Eri2ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"23Eri2_ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"test_23Eri2ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"ba_we23we$"}, typeof(StripeIdException), message)
                .Fail(new object[] {"ba_@$Eri2ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"ba_trEr%2ee23"}, typeof(StripeIdException), message)
                .Succeed(new object[] {"ba_23Eri2ee23"}, message)
                .Succeed(new object[] {"ba_er34ri2ee23"}, message)
                .Assert();
        }
    }
}
