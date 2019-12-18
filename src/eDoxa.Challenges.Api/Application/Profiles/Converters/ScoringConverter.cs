// Filename: ScoringResponseConverter.cs
// Date Created: 2019-12-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;

using Google.Protobuf.Collections;

namespace eDoxa.Challenges.Api.Application.Profiles.Converters
{
    internal sealed class ScoringConverter : IValueConverter<IScoring, MapField<string, float>>
    {
        public MapField<string, float> Convert(IScoring scoring, ResolutionContext context)
        {
            var response = new MapField<string, float>();

            foreach (var (statName, statWeighting) in scoring)
            {
                response.Add(statName, statWeighting);
            }

            return response;
        }
    }
}
