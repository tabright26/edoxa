// Filename: ScoringResponseConverter.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Responses;

namespace eDoxa.Challenges.Api.Application.Profiles.Converters
{
    internal sealed class ScoringResponseConverter : IValueConverter<IScoring, ScoringResponse>
    {
        public ScoringResponse Convert(IScoring scoring, ResolutionContext context)
        {
            var response = new ScoringResponse();

            foreach (var (statName, statWeighting) in scoring)
            {
                response.Add(statName, statWeighting);
            }

            return response;
        }
    }
}
