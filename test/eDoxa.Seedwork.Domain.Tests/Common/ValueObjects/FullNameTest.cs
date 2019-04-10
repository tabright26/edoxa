// Filename: FullNameTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Common.ValueObjects;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Domain.Tests.Common.ValueObjects
{
    [TestClass]
    public sealed class FullNameTest
    {
        [TestMethod]
        [Description("")]
        [DataRow("John", "Kennedy", null)]
        public void T3_FullName(string firstName, string lastName, string middleName)
        {
            // Act
            var fullName = new Name();
            fullName.Change(firstName, lastName);

            // Assert
            Assert.AreEqual(firstName, fullName.FirstName);
            Assert.AreEqual(lastName, fullName.LastName);
        }

        [TestMethod]
        [Description("")]
        public void T6_Default()
        {
            // Act
            var fullName = new Name();

            // Assert
            Assert.IsNull(fullName.FirstName);
            Assert.IsNull(fullName.LastName);
        }

        [TestMethod]
        [Description("")]
        public void T8_FullName()
        {
            // Arrange
            var fullName = new Name();
            fullName.Change("John", "Kennedy");

            // Act
            var toString = fullName.ToString();

            // Assert
            Assert.AreEqual("John Kennedy", toString);
        }
    }
}