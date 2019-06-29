// Filename: ChallengeSynchronizationMoreThanSpecification.cs
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
using eDoxa.Arena.Challenges.Domain.Extensions;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Arena.Challenges.Domain.Specifications
{
    public sealed class LastSynchronizationMoreThanSpecification : Specification<Challenge>
    {
        private readonly TimeSpan _synchronizationInterval;

        public LastSynchronizationMoreThanSpecification(TimeSpan synchronizationInterval)
        {
            _synchronizationInterval = synchronizationInterval;
        }

        public override Expression<Func<Challenge, bool>> ToExpression()
        {
            return challenge => challenge.LastSynchronizationMoreThan(_synchronizationInterval);
        }
    }
}
