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
    internal sealed class MatchesResolver : IMemberValueResolver<Participant, ParticipantDto, IReadOnlyCollection<IMatch>, RepeatedField<MatchDto>>
    {
        public RepeatedField<MatchDto> Resolve(
            Participant participant,
            ParticipantDto participantResponse,
            IReadOnlyCollection<IMatch> matches,
            RepeatedField<MatchDto> matchResponses,
            ResolutionContext context
        )
        {
            foreach (var match in matches)
            {
                var matchResponse = context.Mapper.Map<MatchDto>(match);

                matchResponse.ParticipantId = participant.Id;

                matchResponses.Add(matchResponse);
            }

            return matchResponses;
        }
    }
}
