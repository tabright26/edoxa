// Filename: UserGameReferenceIsProvidedSpecification.cs
// Date Created: 2019-06-01
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
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Arena.Challenges.Domain.Specifications
{
    public sealed class UserGameReferenceIsProvidedSpecification : Specification<Challenge>
    {
        private readonly UserGameReference _userGameReference;

        public UserGameReferenceIsProvidedSpecification(UserGameReference userGameReference)
        {
            _userGameReference = userGameReference;
        }

        public override Expression<Func<Challenge, bool>> ToExpression()
        {
            return challenge => _userGameReference != null;
        }
    }
}
