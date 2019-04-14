// Filename: ChallengesDbContextTest.cs
// Date Created: 2019-03-07
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Infrastructure.Tests
{
    [TestClass]
    public sealed class ChallengesDbContextFactoryTest
    {
        [TestMethod]
        public void Database_CanConnect_ShouldBeTrue()
        {
            using (var factory = new InMemoryDbContextFactory<ChallengesDbContext>())
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