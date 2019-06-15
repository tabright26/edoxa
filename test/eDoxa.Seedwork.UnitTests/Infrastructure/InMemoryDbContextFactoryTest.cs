// Filename: InMemoryDbContextFactoryTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Factories;

using FluentAssertions;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.Infrastructure
{
    [TestClass]
    public sealed class InMemoryDbContextFactoryTest
    {
        [TestMethod]
        public void Database_CanConnect_ShouldBeTrue()
        {
            using (var factory = new InMemoryDbContextFactory<MockCustomDbContext>())
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

        private sealed class MockCustomDbContext : CustomDbContext
        {
            public MockCustomDbContext(DbContextOptions<MockCustomDbContext> options, IMediator mediator) : base(options, mediator)
            {
            }
        }
    }
}
