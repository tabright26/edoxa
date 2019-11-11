// Filename: ParticipantResponsesResolver.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Responses;

namespace eDoxa.Challenges.Api.Profiles.Resolvers
{
    internal sealed class
        ParticipantResponsesResolver : IMemberValueResolver<IChallenge, ChallengeResponse, IReadOnlyCollection<Participant>, ParticipantResponse[]>
    {
        public ParticipantResponse[] Resolve(
            IChallenge challenge,
            ChallengeResponse challengeResponse,
            IReadOnlyCollection<Participant> participants,
            ParticipantResponse[] participantResponses,
            ResolutionContext context
        )
        {
            var participantCount = challenge.Participants.Count;

            participantResponses ??= new ParticipantResponse[participantCount];

            for (var index = 0; index < participantCount; index++)
            {
                var participant = participants.ElementAt(index);

                var participantResponse = context.Mapper.Map<ParticipantResponse>(participant);

                participantResponse.Score = participant.ComputeScore(challenge.BestOf)?.ToDecimal();

                participantResponse.ChallengeId = challenge.Id;

                participantResponses[index] = participantResponse;
            }

            return participantResponses;
        }
    }
}
