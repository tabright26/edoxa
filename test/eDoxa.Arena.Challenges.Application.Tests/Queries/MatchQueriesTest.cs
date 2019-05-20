// Filename: MatchQueriesTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Domain.Factories;
using eDoxa.Arena.Challenges.DTO.Factories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.Application.Tests.Queries
{
    [TestClass]
    public sealed class MatchQueriesTest
    {
        private static readonly FakeDefaultChallengeFactory FakeDefaultChallengeFactory = FakeDefaultChallengeFactory.Instance;
        private static readonly ChallengesMapperFactory ChallengesMapperFactory = ChallengesMapperFactory.Instance;
    }
}