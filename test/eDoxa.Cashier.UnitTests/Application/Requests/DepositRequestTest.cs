// Filename: DepositRequestTest.cs
// Date Created: 2019-06-25
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
    public sealed class DepositRequestTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            TestConstructor<DepositRequest>.ForParameters(typeof(string), typeof(decimal))
                .WithClassName("DepositRequest")
                .WithClassAttributes(typeof(DataContractAttribute))
                .Assert();
        }
    }
}
