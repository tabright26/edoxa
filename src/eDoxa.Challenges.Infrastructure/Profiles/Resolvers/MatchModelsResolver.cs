// Filename: MatchModelsResolver.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Infrastructure.Profiles.Resolvers
{
    internal sealed class MatchModelsResolver : IMemberValueResolver<Participant, ParticipantModel, IReadOnlyCollection<IMatch>, ICollection<MatchModel>>
    {
        public ICollection<MatchModel> Resolve(
            Participant source,
            ParticipantModel destination,
            IReadOnlyCollection<IMatch> sourceMember,
            ICollection<MatchModel> destMember,
            ResolutionContext context
        )
        {
            var matches = context.Mapper.Map<ICollection<MatchModel>>(sourceMember);

            foreach (var match in matches)
            {
                match.Participant = destination;
            }

            return matches;
        }
    }
}
