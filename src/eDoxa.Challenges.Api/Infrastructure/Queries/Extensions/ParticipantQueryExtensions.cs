﻿// Filename: ParticipantQueryExtensions.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.Queries;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Challenges.Responses;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Api.Infrastructure.Queries.Extensions
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

        public static async Task<ParticipantModel?> FindParticipantModelAsync(this IParticipantQuery participantQuery, ParticipantId participantId)
        {
            var participant = await participantQuery.FindParticipantAsync(participantId);

            return participantQuery.Mapper.Map<ParticipantModel>(participant);
        }

        public static async Task<IReadOnlyCollection<ParticipantResponse>> FetchChallengeParticipantResponsesAsync(
            this IParticipantQuery participantQuery,
            ChallengeId challengeId
        )
        {
            var participants = await participantQuery.FetchChallengeParticipantsAsync(challengeId);

            return participantQuery.Mapper.Map<IReadOnlyCollection<ParticipantResponse>>(participants);
        }

        public static async Task<ParticipantResponse?> FindParticipantResponseAsync(this IParticipantQuery participantQuery, ParticipantId participantId)
        {
            var participant = await participantQuery.FindParticipantAsync(participantId);

            return participantQuery.Mapper.Map<ParticipantResponse>(participant);
        }
    }
}