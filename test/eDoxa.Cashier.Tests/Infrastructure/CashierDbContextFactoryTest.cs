// Filename: CashierDbContextFactoryTest.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Tests.Infrastructure
{
    [TestClass]
    public sealed class CashierDbContextFactoryTest
    {
        [TestMethod]
        public void Database_CanConnect_ShouldBeTrue()
        {
            using (var factory = new InMemoryDbContextFactory<CashierDbContext>())
            {
                using (var context = factory.CreateContext())
                {
                    // Act
                    var canConnect = context.Database.CanConnect();

                    // Assert
                    canConnect.Should().BeTrue();
                }
            }
        }
    }
}
