// Filename: VerifyBankAccountCommandTest.cs
// Date Created: 2019-05-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Application.Commands;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.Commands
{
    [TestClass]
    public sealed class VerifyBankAccountCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<VerifyBankAccountCommand>.For()
                .WithName("VerifyBankAccountCommand")
                .Assert();
        }
    }
}