// Filename: UserClaimReplacedIntegrationEventTest.cs
// Date Created: 2019-05-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Application.IntegrationEvents;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.IntegrationEvents
{
    [TestClass]
    public sealed class UserClaimReplacedIntegrationEventTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<UserClaimReplacedIntegrationEvent>
                .For(typeof(Guid), typeof(int), typeof(IDictionary<string, string>), typeof(IDictionary<string, string>))
                .WithName("UserClaimReplacedIntegrationEvent")
                .Assert();
        }
    }
}
