// Filename: ChallengeIsFullSpecification.cs
// Date Created: 2019-04-22
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
    public sealed class ChallengeRegisterIsAvailableSpecification : Specification<Challenge>
    {
        public override Expression<Func<Challenge, bool>> ToExpression()
        {
            return challenge => challenge.Participants.Count < challenge.Setup.Entries;
        }
    }
}