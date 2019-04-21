using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels;

namespace eDoxa.Challenges.Domain
{
    public interface IPrizes : IReadOnlyList<Prize>
    {
    }
}
