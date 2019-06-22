// Filename: ChallengeQueryTest.cs
// Date Created: 2019-06-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.Infrastructure.Fakers;
using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.UnitTests.Asserts;
using eDoxa.Arena.Challenges.UnitTests.Utilities;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Queries
{
    [TestClass]
    public sealed class ChallengeQueryTest
    {
        private static readonly IMapper Mapper = MapperBuilder.CreateMapper();

        [TestMethod]
        public void M()
        {
            // Arrange
            var challengeFaker = new ChallengeFaker();

            var challenge = challengeFaker.Generate();

            // Act
            var challengeViewModel = Mapper.Map<ChallengeViewModel>(challenge);

            // Assert
            ChallengeQueryAssert.IsMapped(challengeViewModel);
        }
    }
}
