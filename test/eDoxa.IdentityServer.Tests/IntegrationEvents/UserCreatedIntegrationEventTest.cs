// Filename: UserCreatedIntegrationEventTest.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IdentityServer.IntegrationEvents;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.IdentityServer.Tests.IntegrationEvents
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<UserCreatedIntegrationEvent>.For(typeof(Guid), typeof(string), typeof(string), typeof(string))
                .WithName("UserCreatedIntegrationEvent")
                .Assert();
        }
    }
}