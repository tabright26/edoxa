using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.AggregateModels.UserAggregate;

namespace eDoxa.Challenges.Domain
{
    public interface IUserPayoff : IReadOnlyDictionary<UserId, Prize>
    {
    }
}
