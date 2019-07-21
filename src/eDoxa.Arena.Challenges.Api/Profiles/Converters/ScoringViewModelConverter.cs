// Filename: ScoringViewModelConverter.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Converters
{
    internal sealed class ScoringViewModelConverter : IValueConverter<IScoring, ScoringViewModel>
    {
        [NotNull]
        public ScoringViewModel Convert([NotNull] IScoring scoring, [NotNull] ResolutionContext context)
        {
            var scoringViewModel = new ScoringViewModel();

            foreach (var (statName, statWeighting) in scoring)
            {
                scoringViewModel.Add(statName, statWeighting);
            }

            return scoringViewModel;
        }
    }
}
