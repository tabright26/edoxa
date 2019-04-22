using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Domain
{
    public interface IPrizes : IReadOnlyList<Prize>
    {
    }
}
