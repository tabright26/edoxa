// Filename: UserCreatedIntegrationEventTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.IdentityServer.IntegrationEvents;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.IdentityServer.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<UserCreatedIntegrationEvent>.ForParameters(
                    typeof(Guid),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(int),
                    typeof(int),
                    typeof(int)
                )
                .WithClassName("UserCreatedIntegrationEvent")
                .Assert();
        }
    }
}
