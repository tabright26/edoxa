using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels;

namespace eDoxa.Challenges.Domain
{
    public interface IUserPrizes : IReadOnlyDictionary<UserId, Prize>
    {
    }
}
