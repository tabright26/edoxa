using System.Collections.Generic;

using eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain.Entities
{
    public interface IPrizes : IReadOnlyList<Prize>
    {
    }
}
