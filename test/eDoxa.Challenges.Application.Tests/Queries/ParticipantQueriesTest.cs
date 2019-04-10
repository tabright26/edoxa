// Filename: ParticipantQueriesTest.cs
// Date Created: 2019-03-22
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Factories;
using eDoxa.Challenges.DTO.Factories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Application.Tests.Queries
{
    [TestClass]
    public sealed class ParticipantQueriesTest
    {        
        private static readonly ChallengeAggregateFactory _challengeAggregateFactory = ChallengeAggregateFactory.Instance;
        private static readonly ChallengesMapperFactory _mapperFactory = ChallengesMapperFactory.Instance;
    }
}