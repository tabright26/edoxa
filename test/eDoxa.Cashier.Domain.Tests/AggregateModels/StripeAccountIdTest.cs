// Filename: StripeAccountIdTest.cs
// Date Created: 2019-05-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Exceptions;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Domain.Tests.AggregateModels
{
    [TestClass]
    public sealed class StripeAccountIdTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            const string message = "Expected Stripe AccountId is invalid.";

            ConstructorTests<StripeAccountId>.For(typeof(string))
                                             .WithName("StripeAccountId")
                                             .Fail(
                                                 new object[]
                                                 {
                                                     null
                                                 },
                                                 typeof(StripeIdException),
                                                 message
                                             )
                                             .Fail(
                                                 new object[]
                                                 {
                                                     "  "
                                                 },
                                                 typeof(StripeIdException),
                                                 message
                                             )
                                             .Fail(
                                                 new object[]
                                                 {
                                                     "acct_23Eri2_ee23"
                                                 },
                                                 typeof(StripeIdException),
                                                 message
                                             )
                                             .Fail(
                                                 new object[]
                                                 {
                                                     "acct23Eri2ee23"
                                                 },
                                                 typeof(StripeIdException),
                                                 message
                                             )
                                             .Fail(
                                                 new object[]
                                                 {
                                                     "23Eri2_ee23"
                                                 },
                                                 typeof(StripeIdException),
                                                 message
                                             )
                                             .Fail(
                                                 new object[]
                                                 {
                                                     "test_23Eri2ee23"
                                                 },
                                                 typeof(StripeIdException),
                                                 message
                                             )
                                             .Fail(
                                                 new object[]
                                                 {
                                                     "acct_we23we$"
                                                 },
                                                 typeof(StripeIdException),
                                                 message
                                             )
                                             .Fail(
                                                 new object[]
                                                 {
                                                     "acct_@$Eri2ee23"
                                                 },
                                                 typeof(StripeIdException),
                                                 message
                                             )
                                             .Fail(
                                                 new object[]
                                                 {
                                                     "acct_trEr%2ee23"
                                                 },
                                                 typeof(StripeIdException),
                                                 message
                                             )
                                             .Succeed(
                                                 new object[]
                                                 {
                                                     "acct_23Eri2ee23"
                                                 },
                                                 message
                                             )
                                             .Succeed(
                                                 new object[]
                                                 {
                                                     "acct_er34ri2ee23"
                                                 },
                                                 message
                                             )
                                             .Assert();
        }
    }
}
