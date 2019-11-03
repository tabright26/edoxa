// Filename: MatchResponsesResolver.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Api.Areas.Challenges.Responses.Profiles.Resolvers
{
    internal sealed class MatchResponsesResolver : IMemberValueResolver<Participant, ParticipantResponse, IReadOnlyCollection<IMatch>, MatchResponse[]>
    {
        public MatchResponse[] Resolve(
            Participant participant,
            ParticipantResponse participantResponse,
            IReadOnlyCollection<IMatch> matches,
            MatchResponse[] matchResponses,
            ResolutionContext context
        )
        {
            var matchCount = participant.Matches.Count;

            matchResponses ??= new MatchResponse[matchCount];

            for (var index = 0; index < matchCount; index++)
            {
                var match = matches.ElementAt(index);

                var matchResponse = context.Mapper.Map<MatchResponse>(match);

                matchResponse.ParticipantId = participant.Id;

                matchResponses[index] = matchResponse;
            }

            return matchResponses;
        }
    }
}
