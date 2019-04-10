// Filename: NotificationMetadataTest.cs
// Date Created: 2019-04-05
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Notifications.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Domain.Tests.AggregateModels.NotificationAggregate
{
    [TestClass]
    public sealed class NotificationMetadataTest
    {
        private static readonly NotificationAggregateFactory _factory = NotificationAggregateFactory.Instance;

        [TestMethod]
        public void Metadata_ShouldBeAssignableToType()
        {
            // Arrange
            var metadata = _factory.CreateMetadata(
                new string[]
                {
                }
            );

            // Act
            var type = typeof(HashSet<string>);

            // Assert
            metadata.Should().BeAssignableTo(type);
        }
    }
}