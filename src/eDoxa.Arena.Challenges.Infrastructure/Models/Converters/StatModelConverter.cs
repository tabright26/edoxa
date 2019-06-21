using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Models.Converters
{
    internal sealed class StatModelConverter : ITypeConverter<StatModel, Stat>
    {
        [NotNull]
        public Stat Convert([NotNull] StatModel source, [NotNull] Stat destination, [NotNull] ResolutionContext context)
        {
            return new Stat(new StatName(source.Name), new StatValue(source.Value), new StatWeighting(source.Weighting));
        }
    }
}
