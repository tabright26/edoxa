using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class ChallengeStat : ValueObject
    {
        public ChallengeStat(StatName name, StatWeighting weighting) : this()
        {
            Name = name;
            Weighting = weighting;
        }

        private ChallengeStat()
        {
            // Required by EF Core.
        }

        public StatName Name { get; private set; }

        public StatWeighting Weighting { get; private set; }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Name;
            yield return Weighting;
        }

        public override string ToString()
        {
            return $"{Name}={Weighting}";
        }
    }
}
