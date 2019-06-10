// Filename: StatFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Api.Application.Data.Fakers;
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class StatFakerTest
    {
        [TestMethod]
        public void FakeStat_ShouldNotThrow()
        {
            // Arrange
            var statFaker = new StatFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var stat = statFaker.FakeStat();

                    Console.WriteLine(stat.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }
    }
}
