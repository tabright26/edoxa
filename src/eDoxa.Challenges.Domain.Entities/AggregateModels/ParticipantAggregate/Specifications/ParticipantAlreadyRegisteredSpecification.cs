// Filename: ParticipantAlreadyRegisteredSpecification.cs
// Date Created: 2019-04-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Linq.Expressions;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.UserAggregate;
using eDoxa.Specifications;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ParticipantAggregate.Specifications
{
    public sealed class ParticipantAlreadyRegisteredSpecification : Specification<Challenge>
    {
        private readonly UserId _userId;

        public ParticipantAlreadyRegisteredSpecification(UserId userId)
        {
            _userId = userId;
        }

        public override Expression<Func<Challenge, bool>> ToExpression()
        {
            return challenge => challenge.Participants.Any(participant => participant.UserId == _userId);
        }
    }
}