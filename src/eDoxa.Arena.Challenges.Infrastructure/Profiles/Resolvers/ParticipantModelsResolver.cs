// Filename: ParticipantModelsResolver.cs
// Date Created: 2019-06-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using AutoMapper;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.Resolvers
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
