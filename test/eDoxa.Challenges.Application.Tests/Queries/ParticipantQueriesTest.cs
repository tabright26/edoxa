// Filename: ParticipantQueriesTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Challenges.Domain.Entities.Factories;
using eDoxa.Challenges.DTO.Factories;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Challenges.Application.Tests.Queries
{
    [TestClass]
    public sealed class ParticipantQueriesTest
    {
        private static readonly ChallengeAggregateFactory ChallengeAggregateFactory = ChallengeAggregateFactory.Instance;
        private static readonly ChallengesMapperFactory ChallengesMapperFactory = ChallengesMapperFactory.Instance;
    }
}