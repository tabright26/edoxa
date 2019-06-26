// Filename: ChallengeForGameSpecification.cs
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
    public sealed class ChallengeForGameSpecification : Specification<Challenge>
    {
        private readonly ChallengeGame _game;

        public ChallengeForGameSpecification(ChallengeGame game)
        {
            _game = game;
        }

        public override Expression<Func<Challenge, bool>> ToExpression()
        {
            return challenge => _game.Equals(challenge.Game);
        }
    }
}
