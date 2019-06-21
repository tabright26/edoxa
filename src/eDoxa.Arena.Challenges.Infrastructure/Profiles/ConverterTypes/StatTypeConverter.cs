// Filename: StatTypeConverter.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.ConverterTypes
{
    internal sealed class StatTypeConverter : ITypeConverter<StatModel, Stat>
    {
        [NotNull]
        public Stat Convert([NotNull] StatModel source, [NotNull] Stat destination, [NotNull] ResolutionContext context)
        {
            return new Stat(new StatName(source.Name), new StatValue(source.Value), new StatWeighting(source.Weighting));
        }
    }
}
