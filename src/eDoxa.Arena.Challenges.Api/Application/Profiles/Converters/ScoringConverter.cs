// Filename: ScoringConverter.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Application.Profiles.Converters
{
    internal sealed class ScoringConverter : IValueConverter<ICollection<ScoringItemModel>, ScoringViewModel>
    {
        [NotNull]
        public ScoringViewModel Convert([NotNull] ICollection<ScoringItemModel> scoringItemModels, [NotNull] ResolutionContext context)
        {
            var scoringViewModel = new ScoringViewModel();

            scoringItemModels.ForEach(scoringItemModel => scoringViewModel.Add(scoringItemModel.Name, scoringItemModel.Weighting));

            return scoringViewModel;
        }
    }
}
