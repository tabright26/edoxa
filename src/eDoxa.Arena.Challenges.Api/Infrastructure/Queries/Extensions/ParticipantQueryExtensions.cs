// Filename: ParticipantQueryExtensions.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.ViewModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Queries;
using eDoxa.Arena.Challenges.Infrastructure.Models;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Queries.Extensions
{
    public static class ParticipantQueryExtensions
    {
        public static async Task<IReadOnlyCollection<ParticipantModel>> FetchChallengeParticipantModelsAsync(
            this IParticipantQuery participantQuery,
            ChallengeId challengeId
        )
        {
            var participants = await participantQuery.FetchChallengeParticipantsAsync(challengeId);

            return participantQuery.Mapper.Map<IReadOnlyCollection<ParticipantModel>>(participants);
        }

        [ItemCanBeNull]
        public static async Task<ParticipantModel> FindParticipantModelAsync(this IParticipantQuery participantQuery, ParticipantId participantId)
        {
            var participant = await participantQuery.FindParticipantAsync(participantId);

            return participantQuery.Mapper.Map<ParticipantModel>(participant);
        }

        public static async Task<IReadOnlyCollection<ParticipantViewModel>> FetchChallengeParticipantViewModelsAsync(
            this IParticipantQuery participantQuery,
            ChallengeId challengeId
        )
        {
            var participants = await participantQuery.FetchChallengeParticipantsAsync(challengeId);

            return participantQuery.Mapper.Map<IReadOnlyCollection<ParticipantViewModel>>(participants);
        }

        [ItemCanBeNull]
        public static async Task<ParticipantViewModel> FindParticipantViewModelAsync(this IParticipantQuery participantQuery, ParticipantId participantId)
        {
            var participant = await participantQuery.FindParticipantAsync(participantId);

            return participantQuery.Mapper.Map<ParticipantViewModel>(participant);
        }
    }
}
