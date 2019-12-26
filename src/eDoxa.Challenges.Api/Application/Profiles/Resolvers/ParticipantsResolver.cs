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
    internal sealed class ParticipantsResolver : IMemberValueResolver<IChallenge, ChallengeDto, IReadOnlyCollection<Participant>, RepeatedField<ParticipantDto>>
    {
        public RepeatedField<ParticipantDto> Resolve(
            IChallenge challenge,
            ChallengeDto challengeResponse,
            IReadOnlyCollection<Participant> participants,
            RepeatedField<ParticipantDto> participantResponses,
            ResolutionContext context
        )
        {
            foreach (var participant in participants)
            {
                var participantResponse = context.Mapper.Map<ParticipantDto>(participant);

                participantResponse.Score = participant.ComputeScore(challenge.BestOf)?.ToDecimal();

                participantResponse.ChallengeId = challenge.Id;

                participantResponses.Add(participantResponse);
            }

            return participantResponses;
        }
    }
}
