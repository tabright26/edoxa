// Filename: MatchModelsResolver.cs
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
using eDoxa.Seedwork.Domain.Extensions;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Infrastructure.Profiles.Resolvers
{
    internal sealed class MatchModelsResolver : IMemberValueResolver<Participant, ParticipantModel, IReadOnlyCollection<IMatch>, ICollection<MatchModel>>
    {
        [NotNull]
        public ICollection<MatchModel> Resolve(
            [NotNull] Participant source,
            [NotNull] ParticipantModel destination,
            [NotNull] IReadOnlyCollection<IMatch> sourceMember,
            [NotNull] ICollection<MatchModel> destMember,
            [NotNull] ResolutionContext context
        )
        {
            var matches = context.Mapper.Map<ICollection<MatchModel>>(sourceMember);

            matches.ForEach(match => match.Participant = destination);

            return matches;
        }
    }
}
