// Filename: WithdrawalCommandTest.cs
// Date Created: 2019-07-05
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
    public sealed class WithdrawalCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<WithdrawalCommand>.ForParameters(typeof(decimal))
                .WithClassName("WithdrawalCommand")
                .WithClassAttributes(typeof(DataContractAttribute))
                .Assert();
        }
    }
}
