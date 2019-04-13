// Filename: CashierDbContextFactoryTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Infrastructure.Tests
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