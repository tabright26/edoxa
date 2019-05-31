// Filename: DepositMoneyCommandTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Seedwork.Domain.Common.Enumerations;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.Commands
{
    [TestClass]
    public sealed class DepositCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DepositCommand>.For(typeof(decimal), typeof(CurrencyType))
                .WithName("DepositCommand")
                .WithAttributes(typeof(DataContractAttribute))
                .Assert();
        }
    }
}
