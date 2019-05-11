// Filename: UserCreatedIntegrationEventHandlerTest.cs
// Date Created: 2019-05-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Application.IntegrationEvents.Handlers;
using eDoxa.Testing.MSTest;

using MediatR;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.IntegrationEvents.Handlers
{
    [TestClass]
    public sealed class UserCreatedIntegrationEventHandlerTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<UserCreatedIntegrationEventHandler>.For(typeof(IMediator))
                .WithName("UserCreatedIntegrationEventHandler")
                .Assert();
        }
    }
}