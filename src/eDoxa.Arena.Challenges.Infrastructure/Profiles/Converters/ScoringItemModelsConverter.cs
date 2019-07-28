// Filename: ScoringItemModelsConverter.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.Converters
{
    internal sealed class ScoringItemModelsConverter : IValueConverter<IScoring, ICollection<ScoringItemModel>>
    {
        
        public ICollection<ScoringItemModel> Convert( IScoring sourceMember,  ResolutionContext context)
        {
            return sourceMember.Select(
                    scoring => new ScoringItemModel
                    {
                        Name = scoring.Key,
                        Weighting = scoring.Value
                    }
                )
                .ToList();
        }
    }
}
