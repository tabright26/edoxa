using System;
using System.Linq.Expressions;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Specifications;

namespace eDoxa.Arena.Challenges.Domain.Specifications
{
    public sealed class ChallengeOfStateSpecification : Specification<Challenge>
    {
        private readonly ChallengeState _challengeState;

        public ChallengeOfStateSpecification(ChallengeState challengeState)
        {
            _challengeState = challengeState;
        }

        public override Expression<Func<Challenge, bool>> ToExpression()
        {
            return challenge => _challengeState.Equals(challenge.Timeline.State);
        }
    }
}
