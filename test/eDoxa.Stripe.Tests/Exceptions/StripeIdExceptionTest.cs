// Filename: StripeIdExceptionTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Stripe.Exceptions;
using eDoxa.Testing.MSTest.Constructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Stripe.Tests.Exceptions
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
