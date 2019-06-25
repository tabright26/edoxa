// Filename: ChallengeQueryTest.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Application.Fakers;
using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.UnitTests.Extensions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Queries
{
    [TestClass]
    public sealed class ChallengeQueryTest
    {
        [TestMethod]
        public void ChallengeViewModel_Mapping_ShouldBeValid()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            // Act
            var challenge = challengeFaker.GenerateViewModel();

            // Assert
            challenge.AssertMappingIsValid();
        }
    }
}
