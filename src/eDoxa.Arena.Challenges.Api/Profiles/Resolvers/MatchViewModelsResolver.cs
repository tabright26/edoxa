// Filename: MatchViewModelsResolver.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Profiles.Resolvers
{
    internal sealed class MatchViewModelsResolver : IMemberValueResolver<Participant, ParticipantViewModel, IReadOnlyCollection<IMatch>, MatchViewModel[]>
    {
        
        public MatchViewModel[] Resolve(
             Participant participant,
             ParticipantViewModel participantViewModel,
             IReadOnlyCollection<IMatch> matches,
             MatchViewModel[]? matchViewModels,
             ResolutionContext context
        )
        {
            var matchCount = participant.Matches.Count;

            matchViewModels ??= new MatchViewModel[matchCount];

            for (var index = 0; index < matchCount; index++)
            {
                var match = matches.ElementAt(index);

                var matchViewModel = context.Mapper.Map<MatchViewModel>(match);

                matchViewModel.ParticipantId = participant.Id;

                matchViewModels[index] = matchViewModel;
            }

            return matchViewModels;
        }
    }
}
