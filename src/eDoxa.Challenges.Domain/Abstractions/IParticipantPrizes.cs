using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels;

namespace eDoxa.Challenges.Domain.Abstractions
{
    public interface IParticipantPrizes : IReadOnlyDictionary<UserId, Prize>
    {
    }
}
