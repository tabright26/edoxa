using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain
{
    public interface IUserPrizes : IReadOnlyDictionary<UserId, Prize>
    {
    }
}
