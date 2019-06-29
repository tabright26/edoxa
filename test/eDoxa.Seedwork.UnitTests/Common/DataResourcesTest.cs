// Filename: DataResourcesTest.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.Common
{
    [TestClass]
    public sealed class DataResourcesTest
    {
        [TestMethod]
        public void TestUserIds_ShouldHaveCountThousand()
        {
            // Act
            var testUserIds = DataResources.TestUserIds;

            // Assert
            testUserIds.Should().HaveCount(1000);
        }
    }
}
