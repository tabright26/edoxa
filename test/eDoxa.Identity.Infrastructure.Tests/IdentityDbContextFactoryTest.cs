// Filename: IdentityDbContextFactoryTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.Infrastructure.Tests
{
    [TestClass]
    public sealed class IdentityDbContextFactoryTest
    {
        [TestMethod]
        public void Database_CanConnect_ShouldBeTrue()
        {
            using (var factory = new InMemoryDbContextFactory<IdentityDbContext>())
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