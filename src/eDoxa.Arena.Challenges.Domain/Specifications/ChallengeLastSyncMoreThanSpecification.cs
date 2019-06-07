// Filename: ChallengeLastSyncMoreThanSpecification.cs
// Date Created: 2019-06-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq.Expressions;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Arena.Challenges.Domain.Specifications
{
    public sealed class ChallengeLastSyncMoreThanSpecification : Specification<Challenge>
    {
        private readonly TimeSpan _timeSpan;

        public ChallengeLastSyncMoreThanSpecification(TimeSpan timeSpan)
        {
            _timeSpan = timeSpan;
        }

        public override Expression<Func<Challenge, bool>> ToExpression()
        {
            return challenge => challenge.LastSyncMoreThan(_timeSpan);
        }
    }
}
