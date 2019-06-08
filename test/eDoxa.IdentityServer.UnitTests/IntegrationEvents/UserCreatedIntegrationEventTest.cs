// Filename: UserCreatedIntegrationEventTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IdentityServer.IntegrationEvents;
using eDoxa.Seedwork.Testing.Constructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.IdentityServer.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<UserCreatedIntegrationEvent>.For(
                    typeof(Guid),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(int),
                    typeof(int),
                    typeof(int)
                )
                .WithName("UserCreatedIntegrationEvent")
                .Assert();
        }
    }
}
