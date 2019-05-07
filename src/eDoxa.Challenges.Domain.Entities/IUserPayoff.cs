using System.Collections.Generic;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Entities.AggregateModels.UserAggregate;

namespace eDoxa.Challenges.Domain.Entities
{
    public interface IUserPayoff : IReadOnlyDictionary<UserId, Prize>
    {
    }
}
