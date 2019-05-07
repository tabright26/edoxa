using System.Collections.Generic;

using eDoxa.Challenges.Domain.Entities.AggregateModels;

namespace eDoxa.Challenges.Domain.Entities.Abstractions
{
    public interface IParticipantPrizes : IReadOnlyDictionary<UserId, Prize>
    {
    }
}
