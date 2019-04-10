// Filename: BirthDateTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel.DataAnnotations;

using eDoxa.Seedwork.Domain.Common.ValueObjects;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Domain.Tests.Common.ValueObjects
{
    [TestClass]
    public sealed class BirthDateTest
    {
        [TestMethod]
        [Description("")]
        public void T0_BirthDate()
        {
            // Act => Arrange
            var birthDate2016 = new BirthDate();
            birthDate2016.Change(2016, 2, 29);

            // Assert
            Assert.AreEqual(2016, birthDate2016.Year);
            Assert.AreEqual(2, birthDate2016.Month);
            Assert.AreEqual(29, birthDate2016.Day);

            // Act => Arrange
            var birthDate2018 = new BirthDate();
            birthDate2018.Change(2018, 2, 28);

            // Assert
            Assert.AreEqual(2018, birthDate2018.Year);
            Assert.AreEqual(2, birthDate2018.Month);
            Assert.AreEqual(28, birthDate2018.Day);
        }

        [TestMethod]
        [Description("")]
        public void T0_Default()
        {
            // Act
            var birthDate = new BirthDate();

            // Assert
            Assert.AreEqual(0001, birthDate.Year);
            Assert.AreEqual(1, birthDate.Month);
            Assert.AreEqual(1, birthDate.Day);
        }

        [TestMethod]
        [Description("")]
        public void T1_BirthDate()
        {
            // Act => Arrange
            var birthDate = BirthDate.FromDate(new DateTime(2016, 2, 29));

            // Assert
            Assert.AreEqual(2016, birthDate.Year);
            Assert.AreEqual(2, birthDate.Month);
            Assert.AreEqual(29, birthDate.Day);
        }

        [TestMethod]
        [Description("")]
        public void T1_IsLegal_False()
        {
            // Arrange
            var birthDate = new BirthDate();
            birthDate.Change(2016, 2, 29);

            // Act
            var condition = birthDate.IsMajor();

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        [Description("")]
        public void T1_IsLegal_True()
        {
            // Arrange
            var birthDate = new BirthDate();
            birthDate.Change(1995, 5, 6);

            // Act
            var condition = birthDate.IsMajor();

            // Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        [Description("")]
        public void T2_BirthDate()
        {
            // Act 
            var birthDate = new BirthDate();
            birthDate.Change(2018, 2, 28);

            // Arrange
            var date = birthDate.ToDate();

            // Assert
            Assert.AreEqual(2018, date.Year);
            Assert.AreEqual(2, date.Month);
            Assert.AreEqual(28, date.Day);
        }

        [TestMethod]
        [Description("")]
        public void T3_BirthDate()
        {
            // Arrange
            var birthDate = new BirthDate();

            // Act => Arrange
            var exception = Assert.ThrowsException<ValidationException>(() => birthDate.Change(2016, 2, 30));

            var innerException = exception.InnerException;

            Assert.IsInstanceOfType(innerException, typeof(ArgumentOutOfRangeException));
        }

        [TestMethod]
        [Description("")]
        public void T4_BirthDate()
        {
            // Arrange
            var birthDate = new BirthDate();

            // Act => Arrange
            var exception = Assert.ThrowsException<ValidationException>(() => birthDate.Change(2018, 2, 29));

            var innerException = exception.InnerException;

            Assert.IsInstanceOfType(innerException, typeof(ArgumentOutOfRangeException));
        }

        [TestMethod]
        [Description("")]
        public void T5_BirthDate_ToString()
        {
            // Arrange
            var birthDate = new BirthDate();
            birthDate.Change(1995, 05, 06);

            // Act            
            var birthDateToString = birthDate.ToString();

            // Assert
            Assert.AreEqual(birthDate.ToDate().ToString("yyyy-MM-dd"), birthDateToString);
        }
    }
}