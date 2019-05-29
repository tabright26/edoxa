﻿// Filename: DepositTokenCommandTest.cs
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
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.Commands
{
    [TestClass]
    public sealed class DepositTokenCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DepositTokenCommand>.For(typeof(TokenDepositBundleType))
                .WithName("DepositTokenCommand")
                .WithAttributes(typeof(DataContractAttribute))
                .Assert();
        }
    }
}