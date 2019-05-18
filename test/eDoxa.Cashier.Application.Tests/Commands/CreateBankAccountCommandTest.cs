// Filename: CreateBankAccountCommandTest.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.Commands
{
    [TestClass]
    public sealed class CreateBankAccountCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<CreateBankAccountCommand>.For(typeof(string))
                .WithName("CreateBankAccountCommand")
                .WithAttributes(typeof(DataContractAttribute))
                .Assert();
        }
    }
}