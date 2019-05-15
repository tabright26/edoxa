// Filename: CreateBankAccountCommandHandlerTest.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;
using eDoxa.Security.Abstractions;
using eDoxa.ServiceBus;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class CreateBankAccountCommandHandlerTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<CreateBankAccountCommandHandler>.For(typeof(IUserInfoService), typeof(IStripeService), typeof(IIntegrationEventService))
                .WithName("CreateBankAccountCommandHandler")
                .Assert();
        }
    }
}