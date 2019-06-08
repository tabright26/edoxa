// Filename: ExternalAccountIsProvidedSpecification.cs
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
using eDoxa.Arena.Challenges.Domain.AggregateModels.ParticipantAggregate;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Arena.Challenges.Domain.Specifications
{
    public sealed class ExternalAccountIsProvidedSpecification : Specification<Challenge>
    {
        private readonly ExternalAccount _externalAccount;

        public ExternalAccountIsProvidedSpecification(ExternalAccount externalAccount)
        {
            _externalAccount = externalAccount;
        }

        public override Expression<Func<Challenge, bool>> ToExpression()
        {
            return challenge => _externalAccount != null;
        }
    }
}
