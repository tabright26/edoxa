// Filename: VerifyBankAccountCommandHandlerTest.cs
// Date Created: 2019-05-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Application.Commands.Handlers;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.Commands.Handlers
{
    [TestClass]
    public sealed class VerifyBankAccountCommandHandlerTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<VerifyBankAccountCommandHandler>.For()
                .WithName("VerifyBankAccountCommandHandler")
                .Assert();
        }
    }
}