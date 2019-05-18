// Filename: UserClaimAddedIntegrationEventTest.cs
// Date Created: 2019-05-13
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
    public sealed class UserClaimAddedIntegrationEventTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<UserClaimAddedIntegrationEvent>.For(typeof(Guid), typeof(IDictionary<string, string>))
                .WithName("UserClaimAddedIntegrationEvent")
                .Assert();

            ConstructorTests<UserClaimAddedIntegrationEvent>.For(typeof(Guid), typeof(string), typeof(string))
                .WithName("UserClaimAddedIntegrationEvent")
                .Assert();
        }
    }
}