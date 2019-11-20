// Filename: StatTypeConverter.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class StatTypeConverter : ITypeConverter<StatModel, Stat>
    {
        public Stat Convert(StatModel source, Stat destination, ResolutionContext context)
        {
            return new Stat(new StatName(source.Name), new StatValue(source.Value), new StatWeighting(source.Weighting));
        }
    }
}
