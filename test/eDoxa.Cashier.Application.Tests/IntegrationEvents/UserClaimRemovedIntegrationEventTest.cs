// Filename: UserClaimRemovedIntegrationEventTest.cs
// Date Created: 2019-05-15
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
    public sealed class UserClaimRemovedIntegrationEventTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<UserClaimRemovedIntegrationEvent>.For(typeof(Guid), typeof(IDictionary<string, string>))
                .WithName("UserClaimRemovedIntegrationEvent")
                .Assert();

            ConstructorTests<UserClaimRemovedIntegrationEvent>.For(typeof(Guid), typeof(string), typeof(string))
                .WithName("UserClaimRemovedIntegrationEvent")
                .Assert();
        }
    }
}