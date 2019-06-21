using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Converters
{
    internal sealed class StatConverter : ITypeConverter<StatModel, Stat>
    {
        [NotNull]
        public Stat Convert([NotNull] StatModel source, [NotNull] Stat destination, [NotNull] ResolutionContext context)
        {
            return new Stat(new StatName(source.Name), new StatValue(source.Value), new StatWeighting(source.Weighting));
        }
    }
}
