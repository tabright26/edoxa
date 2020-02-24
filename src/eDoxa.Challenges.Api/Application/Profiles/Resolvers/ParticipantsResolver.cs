// Filename: ParticipantsResolver.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Grpc.Protos.Challenges.Dtos;

using Google.Protobuf.Collections;

namespace eDoxa.Challenges.Api.Application.Profiles.Resolvers
{
    internal sealed class ParticipantsResolver : IMemberValueResolver<IChallenge, ChallengeDto, IReadOnlyCollection<Participant>, RepeatedField<ChallengeParticipantDto>>
    {
        public RepeatedField<ChallengeParticipantDto> Resolve(
            IChallenge challenge,
            ChallengeDto challengeResponse,
            IReadOnlyCollection<Participant> participants,
            RepeatedField<ChallengeParticipantDto> participantResponses,
            ResolutionContext context
        )
        {
            foreach (var participant in participants)
            {
                var participantResponse = context.Mapper.Map<ChallengeParticipantDto>(participant);

                participantResponse.Score = participant.ComputeScore(challenge.BestOf)?.ToDecimal();

                participantResponse.ChallengeId = challenge.Id;

                participantResponses.Add(participantResponse);
            }

            return participantResponses;
        }
    }
}
