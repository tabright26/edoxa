// Filename: StripeIdExceptionTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Testing.Constructor;
using eDoxa.Stripe.Exceptions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Stripe.UnitTests.Exceptions
{
    [TestClass]
    public sealed class StripeIdExceptionTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<StripeIdException>.For(typeof(string), typeof(Type)).WithName("StripeIdException").Assert();
        }
    }
}
