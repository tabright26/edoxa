// Filename: MatchesResolver.cs
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
    internal sealed class MatchesResolver : IMemberValueResolver<Participant, ChallengeParticipantDto, IReadOnlyCollection<IMatch>, RepeatedField<ChallengeMatchDto>>
    {
        public RepeatedField<ChallengeMatchDto> Resolve(
            Participant participant,
            ChallengeParticipantDto participantResponse,
            IReadOnlyCollection<IMatch> matches,
            RepeatedField<ChallengeMatchDto> matchResponses,
            ResolutionContext context
        )
        {
            foreach (var match in matches)
            {
                var matchResponse = context.Mapper.Map<ChallengeMatchDto>(match);

                matchResponse.ParticipantId = participant.Id;

                matchResponses.Add(matchResponse);
            }

            return matchResponses;
        }
    }
}
