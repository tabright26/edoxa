// Filename: IdentityStorageTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Identity.Api.Infrastructure.Data.Storage;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class IdentityStorageTest
    {
        [TestMethod]
        public void RoleClaims_ShouldHaveRecordCountOfOne()
        {
            // Arrange
            const int recordCount = 1;

            // Act
            var testUsers = IdentityStorage.RoleClaims.ToList();

            // Assert
            testUsers.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void Roles_ShouldHaveRecordCountOfTwo()
        {
            // Arrange
            const int recordCount = 1;

            // Act
            var testUsers = IdentityStorage.Roles.ToList();

            // Assert
            testUsers.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestUserRoles_ShouldHaveRecordCountOfOne()
        {
            // Arrange
            const int recordCount = 1;

            // Act
            var testUserRoles = IdentityStorage.TestUserRoles.ToList();

            // Assert
            testUserRoles.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestUserClaims_ShouldHaveRecordCountOfTwo()
        {
            // Arrange
            const int recordCount = 2;

            // Act
            var testUsers = IdentityStorage.TestUserClaims.ToList();

            // Assert
            testUsers.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestUsers_ShouldHaveCountOfThousandRecords()
        {
            // Arrange
            const int recordCount = 1000;

            // Act
            var testUsers = IdentityStorage.TestUsers.ToList();

            // Assert
            testUsers.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestAdmin_ShouldBeTestAdminId()
        {
            // Act
            var testAdmin = IdentityStorage.TestAdmin;

            // Assert
            testAdmin.Id.Should().Be(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));
        }
    }
}
