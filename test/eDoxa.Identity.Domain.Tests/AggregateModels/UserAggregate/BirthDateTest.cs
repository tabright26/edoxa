﻿// Filename: BirthDateTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.Domain.Tests.AggregateModels.UserAggregate
{
    [TestClass]
    public sealed class BirthDateTest
    {
        [TestMethod]
        public void T1_BirthDate()
        {
            // Act => Arrange
            var birthDate = BirthDate.FromDate(new DateTime(2016, 2, 29));

            // Assert
            Assert.AreEqual(2016, birthDate.Year);
            Assert.AreEqual(2, birthDate.Month);
            Assert.AreEqual(29, birthDate.Day);
        }
    }
}