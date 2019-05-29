// Filename: StripeCustomerIdTest.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Exceptions;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.AggregateModels
{
    [TestClass]
    public sealed class StripeCustomerIdTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "Expected Stripe CustomerId is invalid.";

            ConstructorTests<StripeCustomerId>.For(typeof(string))
                .WithName("StripeCustomerId")
                .Fail(new object[] {null}, typeof(StripeIdException), message)
                .Fail(new object[] {"  "}, typeof(StripeIdException), message)
                .Fail(new object[] {"cus_23Eri2_ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"cus23Eri2ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"23Eri2_ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"test_23Eri2ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"cus_we23we$"}, typeof(StripeIdException), message)
                .Fail(new object[] {"cus_@$Eri2ee23"}, typeof(StripeIdException), message)
                .Fail(new object[] {"cus_trEr%2ee23"}, typeof(StripeIdException), message)
                .Succeed(new object[] {"cus_23Eri2ee23"}, message)
                .Succeed(new object[] {"cus_er34ri2ee23"}, message)
                .Assert();
        }
    }
}
