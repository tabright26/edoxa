// Filename: ParticipantViewModelsResolver.cs
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

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Profiles.Resolvers
{
    internal sealed class
        ParticipantViewModelsResolver : IMemberValueResolver<IChallenge, ChallengeViewModel, IReadOnlyCollection<Participant>, ParticipantViewModel[]>
    {
        [NotNull]
        public ParticipantViewModel[] Resolve(
            [NotNull] IChallenge challenge,
            [NotNull] ChallengeViewModel challengeViewModel,
            [NotNull] IReadOnlyCollection<Participant> participants,
            [CanBeNull] ParticipantViewModel[] participantViewModels,
            [NotNull] ResolutionContext context
        )
        {
            var participantCount = challenge.Participants.Count;

            participantViewModels = participantViewModels ?? new ParticipantViewModel[participantCount];

            for (var index = 0; index < participantCount; index++)
            {
                var participant = participants.ElementAt(index);

                var participantViewModel = context.Mapper.Map<ParticipantViewModel>(participant);

                participantViewModel.Score = participant.ComputeScore(challenge.Setup.BestOf)?.ToDecimal();

                participantViewModel.ChallengeId = challenge.Id;

                participantViewModels[index] = participantViewModel;
            }

            return participantViewModels;
        }
    }
}
