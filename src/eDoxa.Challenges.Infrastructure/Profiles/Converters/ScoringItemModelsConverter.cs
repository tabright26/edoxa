// Filename: ScoringItemModelsConverter.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Profiles.Converters
{
    internal sealed class ScoringItemModelsConverter : IValueConverter<IScoring, ICollection<ScoringItemModel>>
    {
        public ICollection<ScoringItemModel> Convert(IScoring sourceMember, ResolutionContext context)
        {
            return sourceMember.Select(
                    scoring => new ScoringItemModel
                    {
                        Name = scoring.Key,
                        Weighting = scoring.Value
                    })
                .ToList();
        }
    }
}
