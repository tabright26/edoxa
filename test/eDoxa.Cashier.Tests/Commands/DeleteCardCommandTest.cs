// Filename: DeleteCardCommandTest.cs
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
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Testing.MSTest;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.Commands
{
    [TestClass]
    public sealed class DeleteCardCommandTest
    {
        [TestMethod]
        public void Constructor_Tests()
        {
            ConstructorTests<DeleteCardCommand>.For(typeof(StripeCardId)).WithName("DeleteCardCommand").WithAttributes(typeof(DataContractAttribute)).Assert();
        }
    }
}
