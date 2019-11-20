// Filename: ParticipantModelsResolver.cs
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
    internal sealed class
        ParticipantModelsResolver : IMemberValueResolver<IChallenge, ChallengeModel, IReadOnlyCollection<Participant>, ICollection<ParticipantModel>>
    {
        public ICollection<ParticipantModel> Resolve(
            IChallenge source,
            ChallengeModel destination,
            IReadOnlyCollection<Participant> sourceMember,
            ICollection<ParticipantModel> destMember,
            ResolutionContext context
        )
        {
            var participants = context.Mapper.Map<ICollection<ParticipantModel>>(sourceMember);

            foreach (var participant in participants)
            {
                participant.Challenge = destination;
            }

            return participants;
        }
    }
}
