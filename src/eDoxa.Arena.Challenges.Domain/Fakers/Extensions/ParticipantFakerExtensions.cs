// Filename: ParticipantFakerExtensions.cs
// Date Created: 2019-06-13
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

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;

namespace eDoxa.Arena.Challenges.Domain.Fakers.Extensions
{
    public static class ParticipantFakerExtensions
    {
        public static ParticipantId ParticipantId(this Faker faker)
        {
            return AggregateModels.ParticipantAggregate.ParticipantId.FromGuid(faker.Random.Guid());
        }

        public static DateTime ParticipantTimestamp(this Faker faker, IEnumerable<Match> matches)
        {
            return faker.Date.Recent(1, matches.Min(match => match.Timestamp as DateTime?));
        }
    }
}
