﻿// Filename: DepositMoneyCommandTest.cs
// Date Created: 2019-05-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.Commands
{
    [TestClass]
    public sealed class DepositMoneyCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DepositMoneyCommand>.For(typeof(MoneyDepositBundleType))
                .WithName("DepositMoneyCommand")
                .WithAttributes(typeof(DataContractAttribute))
                .Assert();
        }
    }
}