// Filename: ScoringViewModelConverter.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Api.Profiles.Converters
{
    internal sealed class ScoringViewModelConverter : IValueConverter<IScoring, ScoringViewModel>
    {
        
        public ScoringViewModel Convert( IScoring scoring,  ResolutionContext context)
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
