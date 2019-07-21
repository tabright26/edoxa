// Filename: DepositCommandTest.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using eDoxa.Cashier.Api.Application.Commands;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Commands
{
    [TestClass]
    public sealed class DepositCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<DepositCommand>.ForParameters(typeof(string), typeof(decimal))
                .WithClassName("DepositCommand")
                .WithClassAttributes(typeof(DataContractAttribute))
                .Assert();
        }
    }
}
