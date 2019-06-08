// Filename: WithdrawCommandTest.cs
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
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Commands
{
    [TestClass]
    public sealed class WithdrawCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<WithdrawCommand>.ForParameters(typeof(decimal)).WithClassName("WithdrawCommand").WithClassAttributes(typeof(DataContractAttribute)).Assert();
        }
    }
}
