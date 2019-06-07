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
