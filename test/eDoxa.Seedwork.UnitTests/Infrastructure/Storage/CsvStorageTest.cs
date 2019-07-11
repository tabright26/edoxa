// Filename: DataResourcesTest.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Infrastructure.Storage;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.Infrastructure.Storage
{
    [TestClass]
    public sealed class CsvStorageTest
    {
        [TestMethod]
        public void TestUsers_ShouldHaveCountThousand()
        {
            // Act
            var testUsers = CsvStorage.TestUsers;

            // Assert
            testUsers.Should().HaveCount(1000);
        }
    }
}
