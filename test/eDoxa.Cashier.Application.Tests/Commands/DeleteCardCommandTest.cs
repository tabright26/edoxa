﻿// Filename: DeleteCardCommandTest.cs
// Date Created: 2019-05-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Runtime.Serialization;

using eDoxa.Cashier.Application.Commands;
using eDoxa.Cashier.Domain.Services.Stripe.Models;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Application.Tests.Commands
{
    [TestClass]
    public sealed class DeleteCardCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DeleteCardCommand>.For(typeof(CardId))
                .WithName("DeleteCardCommand")
                .WithAttributes(typeof(DataContractAttribute))
                .Assert();
        }
    }
}