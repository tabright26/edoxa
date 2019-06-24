// Filename: ScoringConverter.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Converters
{
    internal sealed class ScoringConverter : IValueConverter<ChallengeModel, ScoringViewModel>
    {
        [NotNull]
        public ScoringViewModel Convert([NotNull] ChallengeModel challenge, [NotNull] ResolutionContext context)
        {
            var scoring = new ScoringViewModel();

            challenge.ScoringItems.ForEach(item => scoring.Add(item.Name, item.Weighting));

            return scoring;
        }
    }
}
