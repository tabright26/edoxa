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

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.Converters
{
    internal sealed class ScoringItemModelsConverter : IValueConverter<IScoring, ICollection<ScoringItemModel>>
    {
        [NotNull]
        public ICollection<ScoringItemModel> Convert([NotNull] IScoring sourceMember, [NotNull] ResolutionContext context)
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
