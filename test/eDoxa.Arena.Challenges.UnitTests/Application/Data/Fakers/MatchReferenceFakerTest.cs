// Filename: MatchReferenceFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Arena.Challenges.Domain.Fakers;
using eDoxa.Seedwork.Common.Enumerations;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Data.Fakers
{
    public sealed class MatchReferenceFakerTest
    {
        public static IEnumerable<object[]> Data => Game.GetAll().Select(game => new object[] {game});

        [DataTestMethod]
        [DynamicData(nameof(Data))]
        public void FakeMatch_ShouldNotThrow(Game game)
        {
            // Arrange
            var matchFaker = new MatchReferenceFaker();

            // Act
            var action = new Action(() => matchFaker.FakeMatchReference(game));

            // Assert
            action.Should().NotThrow();
        }
    }
}
