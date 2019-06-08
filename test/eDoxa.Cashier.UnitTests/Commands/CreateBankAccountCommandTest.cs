// Filename: CreateBankAccountCommandTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Seedwork.Testing.Constructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Commands
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
