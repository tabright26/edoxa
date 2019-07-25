// Filename: WithdrawalRequestTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

using eDoxa.Cashier.Api.Application.Requests;
using eDoxa.Seedwork.Testing.TestConstructor;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Application.Requests
{
    [TestClass]
    public sealed class WithdrawalRequestTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<WithdrawalRequest>.ForParameters(typeof(decimal))
                .WithClassName("WithdrawalRequest")
                .WithClassAttributes(typeof(DataContractAttribute))
                .Assert();
        }
    }
}
