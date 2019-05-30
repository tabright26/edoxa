// Filename: CustomDbContextTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Infrastructure;

using FluentAssertions;

using MediatR;

using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Seedwork.Tests.Infrastructure
{
    [TestClass]
    public sealed class CustomDbContextTest
    {
        [TestMethod]
        public void Database_CanConnect_ShouldBeTrue()
        {
            MockCustomDbContext(
                nameof(this.Database_CanConnect_ShouldBeTrue),
                context =>
                {
                    // Act
                    var canConnect = context.Database.CanConnect();

                    // Assert
                    canConnect.Should().BeTrue();
                }
            );
        }

        private static void MockCustomDbContext(string databaseName, Action<MockDbContext> initialize)
        {
            var options = new DbContextOptionsBuilder<MockDbContext>().UseInMemoryDatabase(databaseName).Options;

            var mediator = new Mock<IMediator>();

            using (var context = new MockDbContext(options, mediator.Object))
            {
                initialize.Invoke(context);
            }
        }

        private sealed class MockDbContext : CustomDbContext
        {
            public MockDbContext(DbContextOptions<MockDbContext> options, IMediator mediator) : base(options, mediator)
            {
            }
        }
    }
}
